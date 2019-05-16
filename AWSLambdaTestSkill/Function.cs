using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlexaSDK;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaTestSkill
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public AlexaResponse FunctionHandler(AlexaRequest input, ILambdaContext context)
        {
            var response = input.GetResponse();
            response.Title = "This is Test Skill";

            context.Logger.LogLine("This is a log message");

            if (input.IsSessionEndedRequest)
            {
                return null;
            }
            else if (input.IsLaunchRequest)
            {
                response.CardText = "What would you like to do?";
                response.SSML = "Tell me what you want.";
                response.SessionEnd = false;
            }
            else if (input.IntentName.Equals("AddTwoNumbers", StringComparison.InvariantCultureIgnoreCase))
            {
                if (input.Slots.ContainsKey("numberone") && input.Slots.ContainsKey("numbertwo"))
                {
                    int firstNum = int.Parse(input.Slots["numberone"].value);
                    int secondNum = int.Parse(input.Slots["numbertwo"].value);

                    int sum = firstNum + secondNum;

                    response.CardText = $"{firstNum} plus {secondNum} equals {sum}";
                    response.SessionEnd = true;
                }
            }
            else
            {
                response.CardText = "Nothing to do";
                response.SSML = "Nothing to do";
                response.SessionEnd = true;
            }

            return response;
        }
    }
}
