namespace MonopolyGame
{
    public enum ReturnState
    {
        Success = 1,
        ValidateBuyPropertyError = -1,
        PayPriceMismatchError = -2,
        HotelMoreThanOneError = -3,
        LandAlreadyBoughtError = -4,
        MortgageError = -5,
        ValidateSellPropertyError = -6,
        InsufficientMoney = -7
    }
}
