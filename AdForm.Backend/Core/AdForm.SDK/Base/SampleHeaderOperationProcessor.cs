using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace AdForm.SDK
{
    public class SampleHeaderOperationProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            //context.OperationDescription.Operation.Parameters.Add(
            //     new OpenApiParameter
            //     {
            //         Name = HttpRequestHeaders.Accept,
            //         Kind = OpenApiParameterKind.Header,
            //         Type = NJsonSchema.JsonObjectType.String,
            //         IsRequired = true
            //     });
            //context.OperationDescription.Operation.Parameters.Add(
            //   new OpenApiParameter
            //   {
            //       Name = HttpRequestHeaders.CorrelationId,
            //       Kind = OpenApiParameterKind.Header,
            //       Type = NJsonSchema.JsonObjectType.String,
            //       IsRequired = true
            //   });
            context.OperationDescription.Operation.Parameters.Add(
              new OpenApiParameter
              {
                  Name = HttpRequestHeaders.ContentLocation,
                  Kind = OpenApiParameterKind.Header,
                  Type = NJsonSchema.JsonObjectType.String,
                  IsRequired = false
              });
            return true;
        }
    }
}
