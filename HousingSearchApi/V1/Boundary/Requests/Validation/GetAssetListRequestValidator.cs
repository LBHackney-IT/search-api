using FluentValidation;
// TODO: 1 Return when last commit
//using Hackney.Core.Validation;

namespace HousingSearchApi.V1.Boundary.Requests.Validation
{
    public class GetAssetListRequestValidator : AbstractValidator<GetAssetListRequest>
    {
        public GetAssetListRequestValidator()
        {
            // TODO: 1 Return when last commit
            //RuleFor(x => x.SearchText).NotNull()
            //                          .NotEmpty()
            //                          .MinimumLength(2)
            //                          .NotXssString();
            //RuleFor(x => x.PageSize).GreaterThan(0);
            //RuleFor(x => x.SortBy).NotXssString();
        }
    }
}
