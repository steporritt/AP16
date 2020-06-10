using System;
using System.Collections.Generic;
using System.Text;

namespace AP16
{
    class Employee
    {
        // In this instance I've chosen to store the Id as a string because I don't know the possible values for Id
        // e.g. it is not unreasonable that employee Ids may start with a '0'.
        public string Id { get; set; }

        // I would consider what the additional properties were and if they were required.
        // For the purposes of this exercise, I have chosen to store the remainder of the employee line
        // as a single string
        public string AdditionalInformation { get; set; }
    }
}
