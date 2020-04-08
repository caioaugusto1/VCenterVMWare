using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCommon
{
    public interface ISample
    {
        /// <summary>
        /// Runs a specific sample. Samples are required to validate user
        /// input and provide usage help message.
        /// </summary>
        /// <param name="args">argument(s) to the sample.</param>
        void RunSample(params string[] args);
    }
}
