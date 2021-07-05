using Apsis.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Interfaces
{
    public interface ICreditCardService
    {
        Task<bool> WithdrawMoney(CreditCardDto creditCard);
    }
}
