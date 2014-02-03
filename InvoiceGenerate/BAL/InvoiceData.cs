using System;

namespace InvoiceGenerate.BAL
{
    public class InvoiceData
    {

        /// <summary>
        /// The _ start date
        /// </summary>
        private DateTime _StartDate;
        /// <summary>
        /// The _ end date
        /// </summary>
        private DateTime _EndDate;
        /// <summary>
        /// The _ generate date
        /// </summary>
        private DateTime _GenerateDate;
        /// <summary>
        /// The _ due date
        /// </summary>
        private DateTime _DueDate;

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        /// <summary>
        /// Gets or sets the generate date.
        /// </summary>
        /// <value>
        /// The generate date.
        /// </value>
        public DateTime GenerateDate
        {
            get { return _GenerateDate; }
            set { _GenerateDate = value; }
        }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
    }
}
