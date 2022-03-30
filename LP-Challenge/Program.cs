// See https://aka.ms/;new-console-template for more information
using LP_Challenge;
Console.WriteLine("Hello, LP-Challenge!");
Console.WriteLine("I used the Pallet and Item's Volume comparison mechanism");

int palletWidth = 48;
int palletHeight = 48;
int palletLength = 48;

int volumeOfPallet = palletWidth * palletHeight * palletLength;

Console.WriteLine("Volume Of Pallet " + volumeOfPallet);

Test1();
Test2();
Test3();

void Test1()
{
    List<ShipmentItem> shipmentItems = new List<ShipmentItem>();

    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = true });

    int requiredPallets = CalculatePalletsCount(shipmentItems, volumeOfPallet);
    Console.WriteLine("Test 1 Required Pallets = " + requiredPallets);
}

void Test2()
{
    List<ShipmentItem> shipmentItems = new List<ShipmentItem>();

    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 24, Length = 24, Width = 24, IsStackable = false });

    int requiredPallets = CalculatePalletsCount(shipmentItems, volumeOfPallet);
    Console.WriteLine("Test 2 Required Pallets = " + requiredPallets);
}

void Test3()
{
    List<ShipmentItem> shipmentItems = new List<ShipmentItem>();

    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 50, Width = 20, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 50, Width = 20, IsStackable = true });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });
    shipmentItems.Add(new ShipmentItem() { Height = 20, Length = 20, Width = 20, IsStackable = false });

    int requiredPallets = CalculatePalletsCount(shipmentItems, volumeOfPallet);
    Console.WriteLine("Test 3 Required Pallets = " + requiredPallets);
}

int CalculatePalletsCount(List<ShipmentItem> shipmentItems, int volumeOfPallet)
{
    int requiredPallets = 0;
    decimal remainingPalletVolume = volumeOfPallet;//set the actual volum of Pallet

    try
    {
        List<ShipmentItem> stackableItems = shipmentItems.Where(x => x.IsStackable == true).ToList();//Stackable items fills first
        foreach (ShipmentItem shipmentItem in stackableItems)
        {
            decimal volumeOfShipmentItem = GetVolumeOfShipmentItem(shipmentItem);//get item's volume
            if (remainingPalletVolume == volumeOfPallet)//if remaining volume is same as the actual Pallet volume then assign new Pallet
            {
                requiredPallets++;//add Pallet
            }
            remainingPalletVolume = remainingPalletVolume - volumeOfShipmentItem;//remove the Item's volume from the Pallet's volume
            if (remainingPalletVolume <= 0)//if the remaining pallet volume is 0 or less then 0 then 
            {
                remainingPalletVolume = volumeOfPallet;//it will reassgin the actual volume
            }
        }

        List<ShipmentItem> noStackableItems = shipmentItems.Where(x => x.IsStackable == false).ToList();//Unstackable items fills on last
        foreach (ShipmentItem shipmentItem in noStackableItems)
        {
            decimal volumeOfShipmentItem = GetVolumeOfShipmentItem(shipmentItem);//get item's volume
            if (remainingPalletVolume == volumeOfPallet)//if remaining volume is same as the actual Pallet volume then assign new Pallet
            {
                requiredPallets++;//add Pallet
            }
            remainingPalletVolume = remainingPalletVolume - volumeOfShipmentItem;//remove the Item's volume from the Pallet's volume
            decimal d = volumeOfPallet / remainingPalletVolume;
            if (2 <= d)//stackable check(2 items will not stack)
            {
                remainingPalletVolume = volumeOfPallet;//it will reassgin the actual volume
            }
        }
    }
    catch (Exception ex)
    {
        throw;
    }
    return requiredPallets;
}

decimal GetVolumeOfShipmentItem(ShipmentItem shipmentItem)
{
    decimal volume = shipmentItem.Width * shipmentItem.Length * shipmentItem.Height;
    return volume;
}