namespace oop.Tests;
using Xunit;
using System;
using System.Reflection;
using System.Collections;
using VendingConsoleApp;


public class UnitTest1
{
    [Fact]
    public void MoneyInputCheck()
    {
        var vm = VendingMachine.CreateDefault();
        var balanceField = vm.GetType().GetField("_balance", BindingFlags.NonPublic | BindingFlags.Instance)!;
        balanceField.SetValue(vm, 0m);
        balanceField.SetValue(vm, 100m);
        var newBalance = (decimal)balanceField.GetValue(vm)!;
        Assert.Equal(100m, newBalance);
    }

    [Fact]
    public void PurchaseCheck()
    {
        var vm = VendingMachine.CreateDefault();
        var balanceField = vm.GetType().GetField("_balance", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var machineMoneyField = vm.GetType().GetField("_machineMoney", BindingFlags.NonPublic | BindingFlags.Instance)!;
        balanceField.SetValue(vm, 200m);
        machineMoneyField.SetValue(vm, 0m);
        balanceField.SetValue(vm, 200m - 55m);
        machineMoneyField.SetValue(vm, 55m);
        var balance = (decimal)balanceField.GetValue(vm)!;
        var machineMoney = (decimal)machineMoneyField.GetValue(vm)!;
        Assert.Equal(145m, balance);
        Assert.Equal(55m, machineMoney);
    }

    [Fact]
    public void ReturnChangeCheck()
    {
        var vm = VendingMachine.CreateDefault();
        var balanceField = vm.GetType().GetField("_balance", BindingFlags.NonPublic | BindingFlags.Instance)!;
        balanceField.SetValue(vm, 75m);
        balanceField.SetValue(vm, 0m);
        var balance = (decimal)balanceField.GetValue(vm)!;
        Assert.Equal(0m, balance);
    }

    [Fact]
    public void AdminModeCheck()
    {
        var vm = VendingMachine.CreateDefault();
        var itemsField = vm.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var items = (IList)itemsField.GetValue(vm)!;
        var itemType = items.GetType().GetGenericArguments()[0];
        var newItem = Activator.CreateInstance(itemType, 99, "ляляля", 10m, 5);
        int countBefore = items.Count;
        items.Add(newItem);
        int countAfter = items.Count;
        Assert.Equal(countBefore + 1, countAfter);
    }

}
