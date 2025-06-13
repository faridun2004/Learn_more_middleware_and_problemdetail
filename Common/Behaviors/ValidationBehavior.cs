using FluentValidation;
using MediatR;

namespace RegisterService.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failrules = _validators
                    .Select(V => V.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(failrule => failrule != null)
                    .ToList();
                if (failrules.Count != 0)
                {
                    throw new ValidationException(failrules);
                }
            }
            return next(cancellationToken);
        }
    }
}
