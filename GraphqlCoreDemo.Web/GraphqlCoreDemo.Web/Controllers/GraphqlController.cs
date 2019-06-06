using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphqlCoreDemo.Infrastructure.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GraphqlCoreDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphqlController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly IServiceProvider _serviceProvider;
        private readonly DataLoaderDocumentListener _listener;

        public GraphqlController(ISchema schema, IDocumentExecuter documentExecuter, IServiceProvider serviceProvider, DataLoaderDocumentListener listener)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _serviceProvider = serviceProvider;
            _listener = listener;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };
            executionOptions.Listeners.Add(_serviceProvider.GetRequiredService<DataLoaderDocumentListener>());

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}