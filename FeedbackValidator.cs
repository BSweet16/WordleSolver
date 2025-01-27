using System;
using System.Linq;

static class FeedbackValidator
{
    public static bool IsValidFeedback(string feedback)
    {
        // Feedback must be exactly 5 characters long and contain only G, Y, or X
        return feedback.Length == 5 && feedback.All(c => c == 'G' || c == 'Y' || c == 'X');
    }
}