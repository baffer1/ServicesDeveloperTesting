
using BankingDomain;

using Xunit.Sdk;

namespace Banking.UnitTests;

public class AccountWithdrawals
{
    [Fact]
    public void WithhdrawingMoneyDecreasesTheBalance()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object);
        var openingBalance = account.GetBalance();
        var amountToWithdraw = 100M;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

    [Fact]
    public void WithdrawingAllMoney()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object);
        var openingBalance = account.GetBalance();
        var amountToWithdraw = openingBalance;
        
        account.Withdraw(amountToWithdraw);

        Assert.Equal(0, openingBalance);
    }

    [Fact]
    public void OverdraftDoesNotDecreaseBalance()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object);
        var openingBalance = account.GetBalance();
        var amountToWithdrawn = openingBalance + 1;

        account.Withdraw(amountToWithdrawn);
        try
        {
            account.Withdraw(amountToWithdrawn);
        }
        catch
        {

        }
        finally
        {
            Assert.Equal(openingBalance, account.GetBalance());
        }
    }

    [Fact]
    public void OverdraftThrowsAnException()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object);
        var openingBalance = account.GetBalance();
        var ammountToWithdrawn = openingBalance + 1;

        Assert.Throws<AccountOverdraftException>(() =>
        {
            account.Withdraw(ammountToWithdrawn);
        });
    }
}
