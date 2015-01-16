using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class GetCompletedPackagesWithinDateRangeExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new GetCompletedPackagesWithinDateRangeExample(Props.GetInstance()).Run();
        }

        public GetCompletedPackagesWithinDateRangeExample( Props props ) : this(props.Get("api.url"), props.Get("api.key")) {
        }

        public GetCompletedPackagesWithinDateRangeExample( String apiKey, String apiUrl ) : base( apiKey, apiUrl ) {
        }

        override public void Execute()
        {
            // get the packages completed today
            Console.WriteLine("PackageStatus : {0}, The number of pakcages : {1}", DocumentPackageStatus.DRAFT, getNumberOfPackageByPackageStatus(DocumentPackageStatus.DRAFT, DateTime.Now, DateTime.Now));
            Console.WriteLine("PackageStatus : {0}, The number of pakcages : {1}", DocumentPackageStatus.SENT, getNumberOfPackageByPackageStatus(DocumentPackageStatus.SENT, DateTime.Now, DateTime.Now));
            Console.WriteLine("PackageStatus : {0}, The number of pakcages : {1}", DocumentPackageStatus.DECLINED, getNumberOfPackageByPackageStatus(DocumentPackageStatus.DECLINED, DateTime.Now, DateTime.Now));
            Console.WriteLine("PackageStatus : {0}, The number of pakcages : {1}", DocumentPackageStatus.ARCHIVED, getNumberOfPackageByPackageStatus(DocumentPackageStatus.ARCHIVED, DateTime.Now, DateTime.Now));
            Console.WriteLine("PackageStatus : {0}, The number of pakcages : {1}", DocumentPackageStatus.COMPLETED, getNumberOfPackageByPackageStatus(DocumentPackageStatus.COMPLETED, DateTime.Now, DateTime.Now));
        }

        private int getNumberOfPackageByPackageStatus(DocumentPackageStatus packageStatus, DateTime startDateRange, DateTime endDateRange) {
            Page<DocumentPackage> resultPage = eslClient.PackageService.GetUpdatedPackagesWithinDateRange(packageStatus, new PageRequest(1), startDateRange, endDateRange);
            return resultPage.NumberOfElements;
        }
    }
}

