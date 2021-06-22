using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FakeProductIden.Models
{
    public class Block
    {
        private int Nonce;
        private readonly DateTime TimeStamp;

        public string PreviousHash { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string Hash { get; private set; }
        public string productId { get; set; }

        public Block(string productId, DateTime timeStamp, List<Transaction> transactions, string previousHash = "")
        {
            this.productId = productId;
            TimeStamp = timeStamp;
            Nonce = 0;
            PreviousHash = previousHash;
            Transactions = transactions;
            Hash = MakeHash(getProduct(this.productId));
        }

        private string getProduct(string id)
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

        public string MakeHash(string productInfo)
        {
            SHA256 sha256 = SHA256.Create();

            string data = productInfo + Transactions + TimeStamp + Nonce;
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public void MiningBlock(int proofOfWorkDiff)
        {
            var productInfo = getProduct(this.productId);
            string leadingZeroes = new String('0', proofOfWorkDiff);

            while (Hash.Substring(0, proofOfWorkDiff) != leadingZeroes)
            {
                Nonce++;
                Hash = MakeHash(productInfo);
            }
        }
    }
}