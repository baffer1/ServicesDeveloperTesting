
using BankingDomain;

namespace Banking.UnitTests;

public class BankAccountOverdraftNotifications
{
    [Fact]
    public void ApiIsNotifiedUponOverdraft()
    {
        // Given
        var mockedNotifier = new Mock<INotifyOfOverdrafts>();
        var account = new BankAccount(mockedNotifier.Object); // TODO: Need a mock object.
        var amountToWithdraw = account.GetBalance() + .01M;

        // When I overdraft
        try
        {
            account.Withdraw(amountToWithdraw); // Cause an Overdraft
        }
        catch (Exception)
        {
            // swallow it!
        }


        // THEN

        // Verify the notifier was called with the account and the amount;
        mockedNotifier.Verify(m => m.NotifyOfOverdraftAttempt(account, amountToWithdraw), Times.Once);

    }

    [Fact]
    public void ApiIsNotNotifiedWhenNoOverdraft()
    {
        var mockedNotifier = new Mock<INotifyOfOverdrafts>();
        var account = new BankAccount(mockedNotifier.Object); // TODO: Need a mock object.
        var amountToWithdraw = 1M;

        // When I overdraft

        account.Withdraw(amountToWithdraw); // Cause an Overdraft



        // THEN

        // Verify the notifier was called with the account and the amount;
        mockedNotifier.Verify(m => m.NotifyOfOverdraftAttempt(account, amountToWithdraw), Times.Never);
    }
    [Fact]
    public void IfApiThrowsWriteToTheLogger()
    {
        //
        var stubbedNotifier = new Mock<INotifyOfOverdrafts>();
        var account = new BankAccount(stubbedNotifier.Object); // TODO: Need a mock object.
        var amountToWithdraw = 1M;


        account.Withdraw(amountToWithdraw);

        // Then....
    }
}
