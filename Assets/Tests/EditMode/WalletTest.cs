using NUnit.Framework;
using Project0;
public class WalletTest
{
    [Test]
    public void add_coins_starting_at_0()
    {
        Wallet wallet = new Wallet();

        int gain_amount_to_test = 15;
        wallet.GainCoins(gain_amount_to_test);

        Assert.AreEqual(expected: gain_amount_to_test, actual: wallet.GetCoinCount());
    }

    [Test]
    public void add_coins_not_starting_at_0()
    {
        int walletStart = 20;
        int amountToAdd = 10;
        Wallet wallet = new Wallet(walletStart);

        int testValue = walletStart + amountToAdd;
        wallet.GainCoins(amountToAdd);

        Assert.AreEqual(expected: testValue, actual: wallet.GetCoinCount());
    }

    [Test]
    public void pay_with_having_more_than_amount_in_wallet()
    {
        int walletStart = 20;
        int amountToPay = 7;
        Wallet wallet = new Wallet(walletStart);

        int testValue = walletStart - amountToPay;
        wallet.PayCoins(amountToPay);

        Assert.AreEqual(expected: testValue, actual: wallet.GetCoinCount());
    }

    [Test]
    public void pay_with_having_exact_amount_in_wallet()
    {
        int walletStart = 10;
        int amountToPay = 10;
        Wallet wallet = new Wallet(walletStart);

        int testValue = walletStart - amountToPay;
        wallet.PayCoins(amountToPay);

        Assert.AreEqual(expected: testValue, actual: wallet.GetCoinCount());
    }

    [Test]
    public void pay_with_not_enough_money_in_wallet()
    {
        int walletStart = 10;
        int amountToPay = 15;
        Wallet wallet = new Wallet(walletStart);

        wallet.PayCoins(amountToPay);

        Assert.IsTrue(wallet.GetCoinCount() > 0);
        Assert.AreEqual(expected: walletStart, actual: wallet.GetCoinCount());
    }
}
