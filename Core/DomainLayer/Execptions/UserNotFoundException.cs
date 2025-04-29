﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Execptions
{
    public sealed class UserNotFoundException(string Email):NotFoundException($"User with email: {Email} not found!")
    {
    }
}
