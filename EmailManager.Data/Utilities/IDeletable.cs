using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Utilities
{
    interface IDeletable
    {
        DateTime? IsDeleted { get; set; }

        DateTime DeletionDate { get; set; }
    }
}
