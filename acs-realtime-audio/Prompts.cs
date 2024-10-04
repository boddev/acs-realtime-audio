using System;

namespace acs_realtime_audio;

public static class Prompts
{
    public static string answerPromptSystemTemplate = """ 
    You are an assisant designed to answer the customer query and analyze the sentiment score from the customer tone. 
    You also need to determine the intent of the customer query and classify it into categories such as sales, marketing, shopping, etc.
    Use a scale of 1-10 (10 being highest) to rate the sentiment score. 
    Use the below format, replacing the text in brackets with the result. Do not include the brackets in the output: 
    Content:[Answer the customer query briefly and clearly in two lines and ask if there is anything else you can help with] 
    Score:[Sentiment score of the customer tone] 
    Intent:[Determine the intent of the customer query] 
    Category:[Classify the intent into one of the categories]
    """;

    public static string helloPrompt = "Hello, thank you for calling! How can I help you today?";
    public static string timeoutSilencePrompt = "I'm sorry, I didn't hear anything. If you need assistance please let me know how I can help you.";
    public static string goodbyePrompt = "Thank you for calling! I hope I was able to assist you. Have a great day!";
    public static string connectAgentPrompt = "I'm sorry, I was not able to assist you with your request. Let me transfer you to an agent who can help you further. Please hold the line and I'll connect you shortly.";
    public static string callTransferFailurePrompt = "It looks like all I can't connect you to an agent right now, but we will get the next available agent to call you back as soon as possible.";
    public static string agentPhoneNumberEmptyPrompt = "I'm sorry, we're currently experiencing high call volumes and all of our agents are currently busy. Our next available agent will call you back as soon as possible.";
    public static string EndCallPhraseToConnectAgent = "Sure, please stay on the line. I'm going to transfer you to an agent.";

    public static string transferFailedContext = "TransferFailed";
    public static string connectAgentContext = "ConnectAgent";
    public static string goodbyeContext = "Goodbye";

    public static string chatResponseExtractPattern = @"\s*Content:(.*)\s*Score:(.*\d+)\s*Intent:(.*)\s*Category:(.*)";
}
