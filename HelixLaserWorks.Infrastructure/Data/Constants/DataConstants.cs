namespace HelixLaserWorks.Infrastructure.Data.Constants
{
    public static class DataConstants
    {
        //Material Data Constants
        public const int MaterialNameMaxLength = 50;
        public const int MaterialNameMinLength = 3;

        public const double MaterialThicknessMaxValue = 50.0;
        public const double MaterialThicknessMinValue = 0.01;

        public const int MaterialSpecificationMaxLenth = 100;
        public const int MaterialSpecificationMinLenth = 10;

        public const double MaterialDensityMaxValue = 100.00;
        public const double MaterialDensityMinValue = 0.1;

        public const double MaterialPricePerSquareMeterMaxValue = 10000.00;
        public const double MaterialPricePerSquareMeterMinValue = 1.00;


        //Order Data Constants
        public const int OrderDescriptionMaxLength = 200;
        public const int OrderDescriptionMinLength = 10;


        //Part Data Constants
        public const int PartNameMaxLength = 100;
        public const int PartNameMinLength = 5;

        public const int PartDescriptionMaxLength = 500;

        public const int PartQuantityMaxValue = 9999;

        public const int PartSchemeURLMaxLength = 4000;
        public const int PartSchemeURLMinLength = 10;



        //Review Data Constans
        public const int ReviewCommentMaxLength = 1000;


        //Offer Data Constants
        public const int OfferNotesMaxLength = 1000;
    }
}
