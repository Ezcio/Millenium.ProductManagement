﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Commands
{
    public record DeleteProduct(int Id) : IRequest<bool>;
}
