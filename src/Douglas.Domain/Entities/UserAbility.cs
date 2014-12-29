﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Douglas.Domain.Entities
{
   

    public class UserAbility : Entity
    {

        public virtual string Description { get; protected set; }

        protected UserAbility()
        {
            
        }

        public UserAbility(string description)
        {
            this.Description = description;
            Id = Guid.NewGuid();

        }


    }
}
