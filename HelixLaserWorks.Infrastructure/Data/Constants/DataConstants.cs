namespace HelixLaserWorks.Infrastructure.Data.Constants
{
    public static class DataConstants
    {
        //Material Data Constants
        public const int MaterialNameMaxLength = 50;
        public const int MaterialNameMinLength = 3;

        public const int MaterialDescriptionMaxLength = 800;
        public const int MaterialDescriptionMinLength = 10;

        public const double MaterialThicknessMaxValue = 50.0;
        public const double MaterialThicknessMinValue = 0.01;

        public const int MaterialSpecificationMaxLenth = 100;
        public const int MaterialSpecificationMinLenth = 10;

        public const double MaterialDensityMaxValue = 100.00;
        public const double MaterialDensityMinValue = 0.1;

        public const double MaterialPricePerSquareMeterMaxValue = 10000.00;
        public const double MaterialPricePerSquareMeterMinValue = 1.00;

        //MaterialType Constants
        public const int MaterialTypeNameMaxLength = 20;
        public const int MaterialTypeNameMinLength = 3;


        //Order Constants
        public const int OrderDescriptionMaxLength = 200;
        public const int OrderDescriptionMinLength = 10;

        public const int OrderTitleMaxLength = 50;
        public const int OrderTitleMinLength = 5;

        public const int OrderPhoneMaxLength = 15;
        public const int OrderPhoneMinLength = 6;

        public const int OrderFeedbackMaxLength = 500;
        public const int OrderFeedbackMinLength = 10;


        //Part Constants
        public const int PartNameMaxLength = 100;
        public const int PartNameMinLength = 5;

        public const int PartDescriptionMaxLength = 500;
        public const int PartDescriptionMinLength = 5;

        public const int PartQuantityMinValue = 1;
        public const int PartQuantityMaxValue = 9999;

        public const int PartSchemeURLMaxLength = 4000;
        public const int PartSchemeURLMinLength = 10;

        public const int PartsPerPageDefaultCount = 3;


        //Review Constans
        public const int ReviewCommentMaxLength = 1000;


        //Offer Constants
        public const int OfferNotesMaxLength = 1000;
    }
}
