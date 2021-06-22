using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeProductIden.Models
{
    public class BlockChain
    {
        private readonly int Reward;
        private readonly int ProofOfWorkDiff;
        private List<Transaction> PendingTransactions;

        public List<Block> BChain { get; set; }

        public BlockChain(int reward, int proofOfWorkDiff)
        {
            Reward = reward;
            ProofOfWorkDiff = proofOfWorkDiff;
            PendingTransactions = new List<Transaction>();
            BChain = new List<Block> { GenerateGenesisBlock() };
        }

        private Block GenerateGenesisBlock()
        {
            List<Transaction> transactions = new List<Transaction> { new Transaction("", "", 0) };
            return new Block("", DateTime.Now, transactions, "0");
        }

        public bool IsValidChain()
        {
            for (int i = 1; i < BChain.Count; i++)
            {
                Block prevBlock = BChain[i - 1];
                Block currBlock = BChain[i];
                if (currBlock.Hash != currBlock.MakeHash(getProduct(currBlock.productId)))
                    return false;
                if (currBlock.PreviousHash != prevBlock.Hash)
                    return false;
            }
            return true;
        }


        public void MineBlock(string minerAddress, string productID)
        {
            Transaction minerRewardTransaction = new Transaction(null, minerAddress, Reward);
            PendingTransactions.Add(minerRewardTransaction);
            Block block = new Block(productID, DateTime.Now, PendingTransactions);
            block.MiningBlock(ProofOfWorkDiff);
            block.PreviousHash = BChain.Last().Hash;
            BChain.Add(block);
            PendingTransactions = new List<Transaction>();
        }

        public List<List<string>> GetAll()
        {
            List<List<string>> AllTransactions = new List<List<string>>();
            int i = 0;
            foreach (Block block in this.BChain)
            {
                List<string> OneTransaction = new List<string>();
                OneTransaction.Add("BLOCK NO. " + i.ToString());
                OneTransaction.Add("Item name: " + block.productId);
                OneTransaction.Add("Hash: " + block.Hash);
                OneTransaction.Add("Previous Hash: " + block.PreviousHash);
                i++;
                AllTransactions.Add(OneTransaction);
            }
            return AllTransactions;
        }

        public string getProduct(string id)
        {
            if (id == "")
            {
                return "";
            }
            using (var db = new FakeProductIdenEntities())
            {
                var product = db.Products.Find(id);
                return product.pr_id + "-" + product.pr_name + "-" + product.pr_cmp + "-" + product.pr_type + "-" + product.pr_origin + "-" + product.pr_price.ToString();
            }
                
        }
    }
}