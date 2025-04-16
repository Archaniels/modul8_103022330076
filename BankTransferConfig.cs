using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using static BankTransferConfig;

class BankTransferConfig
{
    //CONFIG1 => “en”
    //CONFIG2 => 25000000
    //CONFIG3 => 6500
    //CONFIG4 => 15000
    //CONFIG5 => [ “RTO(real - time)”, “SKN”, “RTGS”, “BI FAST” ]
    //CONFIG6 => “yes”
    //CONFIG7 => “ya”
    public String lang { get; set; }
    public Transfer transfer { get; set; }
    public String methods { get; set; }
    public Confirmation confirmation { get; set; }
    public class Transfer()
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }
    }
    public class Confirmation()
    {
        public String en { get; set; }
        public String id { get; set; }
    }
    public BankTransferConfig() { }
    public BankTransferConfig(String LANG, int THRESHOLD, int LOW_FEE, int HIGH_FEE, String EN, String ID)
    {
        lang = LANG;
        transfer.threshold = THRESHOLD;
        transfer.low_fee = LOW_FEE;
        transfer.high_fee = HIGH_FEE;
        confirmation.en = EN;
        confirmation.id = ID;
    }
}

class UIConfig
{
    public BankTransferConfig config;
    public const String filePath = @"bank_transfer_config.json";
    public UIConfig()
    {
        try
        {
            ReadConfigFile();
        }
        catch (Exception)
        {
            SetDefault();
            WriteNewConfigFile();
        }
    }
    private BankTransferConfig ReadConfigFile()
    {
        String jsonDATA = File.ReadAllText(filePath);
        var config = JsonSerializer.Deserialize<BankTransferConfig>(jsonDATA);
        //this.lang = data.lang;
        //this.methods = data.methods;
        //this.transfer = data.transfer;
        //this.confirmation = data.confirmation;
        return config;
    }
    private void SetDefault()
    {
        String jsonDATA = File.ReadAllText(filePath);
        var config = JsonSerializer.Deserialize<BankTransferConfig>(jsonDATA);
        config.lang = "en";
        config.transfer.threshold = 25000000;
        config.transfer.low_fee = 6500;
        config.transfer.high_fee = 15000;
        config.methods = "[ “RTO (real-time)”, “SKN”, “RTGS”, “BI FAST” ]";
        config.confirmation.en = "yes";
        config.confirmation.id = "ya";
    }
    private void WriteNewConfigFile()
    {
        if (File.Exists(filePath))
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            String jsonString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(filePath, jsonString);
        }
    }
}

class PROgram
{
    public static void Main(String[] args)
    {
        BankTransferConfig config = new BankTransferConfig();
        UIConfig uIConfig = new UIConfig();

        if (uIConfig.config.lang == "en")
        {
            Console.WriteLine("Please insert the amount of money to transfer: ");
            int jumlahTransfer = Console.Read();
            int biayaTransfer = 0;
            if (jumlahTransfer <= uIConfig.config.transfer.threshold)
            {
                Console.WriteLine("Transfer fee is: " + uIConfig.config.transfer.low_fee);
                biayaTransfer = uIConfig.config.transfer.low_fee;
            }
            else if (jumlahTransfer >= uIConfig.config.transfer.threshold)
            {
                Console.WriteLine("Transfer fee is: " + uIConfig.config.transfer.high_fee);
                biayaTransfer = uIConfig.config.transfer.high_fee;
            }
            Console.WriteLine("Transfer fee = " + biayaTransfer + " dan Total amount = " + jumlahTransfer + biayaTransfer);

            Console.WriteLine("");

            Console.WriteLine("Select transfer method: ");

            for (int i = 0; i < uIConfig.config.methods.Count(); i++)
            {
                Console.WriteLine(i + 1 + " " + uIConfig.config.methods);
            }

            String pilihanMethod = Console.ReadLine();

            Console.WriteLine("Please type " + uIConfig.config.confirmation.en + " to confirm the transaction:");
            String confirm = Console.ReadLine();
            if (confirm == uIConfig.config.confirmation.en)
            {
                Console.WriteLine("The transfer is completed");
            } else
            {
                Console.WriteLine("Transferred is cancelled");
            }

        } else
        {
            Console.WriteLine("Masukkan jumlah uang yang akan di-transfer: ");
            int jumlahTransfer = Console.Read();
            int biayaTransfer = 0;
            if (jumlahTransfer <= uIConfig.config.transfer.threshold)
            {
                Console.WriteLine("Biaya transfer adalah: " + uIConfig.config.transfer.low_fee);
                biayaTransfer = uIConfig.config.transfer.low_fee;
            }
            else if (jumlahTransfer >= uIConfig.config.transfer.threshold)
            {
                Console.WriteLine("Biaya transfer adalah: " + uIConfig.config.transfer.high_fee);
                biayaTransfer = uIConfig.config.transfer.high_fee;
            }
            Console.WriteLine("Biaya transfer = " + biayaTransfer + " dan Total biaya = " + jumlahTransfer + biayaTransfer);
            
            Console.WriteLine("");

            Console.WriteLine("Pilih metode transfer: ");

            for (int i = 0; i < uIConfig.config.methods.Count(); i++)
            {
                Console.WriteLine(i + 1 + " " + uIConfig.config.methods);
            }

            String pilihanMethod = Console.ReadLine();

            Console.WriteLine("Please type " + uIConfig.config.confirmation.id + " to confirm the transaction:");
            String confirm = Console.ReadLine();
            if (confirm == uIConfig.config.confirmation.id)
            {
                Console.WriteLine("Proses transfer berhasil");
            }
            else
            {
                Console.WriteLine("Transfer dibatalkan");
            }
        }
    }
}