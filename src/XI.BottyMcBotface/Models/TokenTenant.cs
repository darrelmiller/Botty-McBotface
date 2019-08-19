using System;

namespace XI.BottyMcBotface.Models
{
    public class TokenTenant
    {

        public int ID { get; set; }

        public string TenantId { get; set; }

        public string Token { get; set; }

        public DateTime ExpireDate { get; set; }

        public string Domain { get; set; }

        public string AdminEmail { get; set; }

    }
}
