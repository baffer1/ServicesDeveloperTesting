namespace BankingDomain;

public class BankAccount
{
    private decimal _balance = 5000m;
    private readonly INotifyOfOverdrafts _overdraftNotifier;
    public BankAccount(INotifyOfOverdrafts overdraftNotifier)
    {
        _overdraftNotifier = overdraftNotifier;
    }

    public void Deposit(decimal amountToDeposit)
    {
        _balance += amountToDeposit;
    }

    public decimal GetBalance()
    {
        return _balance;
    }

    public void Withdraw(decimal amountToWithdraw)
    {
        if (amountToWithdraw > _balance)
        {
            _overdraftNotifier.NotifyOfOverdraftAttempt(this, amountToWithdraw);
            throw new AccountOverdraftException(); 
        }
        else
        { 
            _balance -= amountToWithdraw; 
        }
    }


}