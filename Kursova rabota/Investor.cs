class Investor
{


    string investorName;
    int contractNumber;
    string homeAdress;
    int depositAmount;
    int contractTerm;

    public Investor()
    {

    }

    public Investor(string investorName, int contractNumber, string homeAdress, int depositAmount, int contractTerm)
    {
        InvestorName = investorName;
        ContractNumber = contractNumber;
        HomeAdress = homeAdress;
        DepositAmount = depositAmount;
        ContractTerm = contractTerm;
    }

    public string InvestorName { get => investorName; set => investorName = value; }
    public int ContractNumber { get => contractNumber; set => contractNumber = value; }
    public string HomeAdress { get => homeAdress; set => homeAdress = value; }
    public int DepositAmount { get => depositAmount; set => depositAmount = value; }
    public int ContractTerm { get => contractTerm; set => contractTerm = value; }


    public void Write(BinaryWriter bw)
    {

        bw.Write(investorName);
        bw.Write(contractNumber);
        bw.Write(homeAdress);
        bw.Write(depositAmount);
        bw.Write(contractTerm);

    }

    public static Investor Read(BinaryReader br)
    {
        Investor inv = new Investor();
        inv.InvestorName = br.ReadString();
        inv.ContractNumber = br.ReadInt32();
        inv.HomeAdress = br.ReadString();
        inv.DepositAmount = br.ReadInt32();
        inv.ContractTerm = br.ReadInt32();

        return inv;
    }
}
}
