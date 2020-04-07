using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Services;

namespace SDK.Examples
{
    public class ConfigureHandoverUrlExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ConfigureHandoverUrlExample().Run();
        }

        public readonly string ITALIAN = "it";
        public readonly string TEXT = "text";
        public readonly string HREF = "href";
        public readonly string TITLE = "title";
        public readonly string UPDATED_TITLE = "updated title";

        public Handover handoverBeforeCreating, handoverAfterCreating, handoverAfterUpdating, handoverAfterDeleting;

        override public void Execute()
        {
            AccountConfigService accountConfigService = ossClient.AccountConfigService;
            handoverBeforeCreating = accountConfigService.GetHandoverUrl(ITALIAN);

            handoverAfterCreating = accountConfigService
                .CreateHandoverUrl(HandoverBuilder.NewHandover(ITALIAN)
                    .WithHref(HREF)
                    .WithText(TEXT)
                    .WithTitle(TITLE)
                    .Build());

            handoverAfterUpdating = accountConfigService.UpdateHandoverUrl(HandoverBuilder.NewHandover(ITALIAN)
                .WithTitle(UPDATED_TITLE)
                .Build());

            accountConfigService.DeleteHandoverUrl(ITALIAN);
            handoverAfterDeleting = accountConfigService.GetHandoverUrl(ITALIAN);
        }
    }
}