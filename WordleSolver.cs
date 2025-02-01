using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class WordleSolver
{
    public async Task RunSolver()
    {
        Console.WriteLine("Welcome to the Wordle Solver!");

        // Load a list of known english words
        List<string> wordList = WordList.Words;
        if (wordList == null || wordList.Count == 0)
        {
            Console.WriteLine("Word list is empty. Exiting.");
            return;
        }

        Console.WriteLine("Word list loaded successfully.");

        // Start solving
        SolveWordle(wordList);
    }

    private void SolveWordle(List<string> wordList)
    {
        int attempts = 0;
        bool solved = false;
        List<string> possibleWords = new List<string>(wordList); // Words that could still be the target

        while (attempts < 6 && !solved)
        {
            attempts++;
            if (attempts > 1){
                Console.WriteLine();
            }
            Console.WriteLine($"Attempt #{attempts}");

            // Get the best guess based on the current possible words
            string guess = GetBestGuess(possibleWords);
            Console.WriteLine($"Guess: {guess}");

            // Get feedback from the user
            Console.WriteLine("Enter feedback (G for green, Y for yellow, X for gray): ");
            string feedback = Console.ReadLine().ToUpper();

            // Validate feedback input
            if (!FeedbackValidator.IsValidFeedback(feedback))
            {
                Console.WriteLine("Invalid feedback. Please enter exactly 5 characters (G, Y, or X).");
                attempts--; // Retry the same attempt
                continue;
            }

            // If the feedback is all green, the puzzle is solved
            if (feedback == "GGGGG")
            {
                solved = true;
                Console.WriteLine($"Congratulations! You solved the Wordle in {attempts} attempts.");
                break;
            }

            // Narrow down the list of possible words based on feedback
            possibleWords = NarrowDownWords(possibleWords, guess, feedback);

            if (possibleWords.Count == 0)
            {
                Console.WriteLine("No possible words left. The Wordle may be unsolvable with the given feedback.");
                break;
            }
        }

        if (!solved)
        {
            Console.WriteLine("Out of attempts. The Wordle was not solved.");
        }
    }

    private string GetBestGuess(List<string> possibleWords)
    {
        // Calculate letter frequency in the remaining possible words
        Dictionary<char, int> letterFrequency = new Dictionary<char, int>();
        foreach (string word in possibleWords)
        {
            foreach (char c in word.Distinct()) // Count each letter only once per word
            {
                if (letterFrequency.ContainsKey(c))
                    letterFrequency[c]++;
                else
                    letterFrequency[c] = 1;
            }
        }

        // Find the word with the highest total letter frequency
        string bestGuess = possibleWords[0];
        int maxScore = 0;

        foreach (string word in possibleWords)
        {
            int score = word.Distinct().Sum(c => letterFrequency.ContainsKey(c) ? letterFrequency[c] : 0);
            if (score > maxScore)
            {
                maxScore = score;
                bestGuess = word;
            }
        }

        return bestGuess;
    }

    private List<string> NarrowDownWords(List<string> possibleWords, string guess, string feedback)
    {
        List<string> newPossibleWords = new List<string>();

        foreach (string word in possibleWords)
        {
            bool isValid = true;

            // Track the minimum number of occurrences for each letter in the target word
            Dictionary<char, int> minLetterCounts = new Dictionary<char, int>();

            // Check for green and yellow feedback to determine minimum letter counts
            for (int i = 0; i < 5; i++)
            {
                char guessedChar = guess[i];
                if (feedback[i] == 'G' || feedback[i] == 'Y')
                {
                    if (minLetterCounts.ContainsKey(guessedChar))
                        minLetterCounts[guessedChar]++;
                    else
                        minLetterCounts[guessedChar] = 1;
                }
            }

            // Check for green feedback (correct letter and position)
            for (int i = 0; i < 5; i++)
            {
                if (feedback[i] == 'G' && word[i] != guess[i])
                {
                    isValid = false;
                    break;
                }
            }

            // Check for yellow feedback (correct letter but wrong position)
            for (int i = 0; i < 5; i++)
            {
                if (feedback[i] == 'Y')
                {
                    if (word[i] == guess[i] || !word.Contains(guess[i].ToString()))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            // Check for gray feedback (letter not in the word)
            for (int i = 0; i < 5; i++)
            {
                if (feedback[i] == 'X')
                {
                    // If the letter appears in the word but is marked as gray, it's invalid
                    if (word.Contains(guess[i].ToString()))
                    {
                        // Exception: If the letter appears in the word but is already accounted for by green/yellow feedback, it's valid
                        if (!minLetterCounts.ContainsKey(guess[i]) || word.Count(c => c == guess[i]) > minLetterCounts[guess[i]])
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }

            // Ensure the word contains at least the minimum number of occurrences for each letter
            foreach (var kvp in minLetterCounts)
            {
                if (word.Count(c => c == kvp.Key) < kvp.Value)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
                newPossibleWords.Add(word);
        }

        return newPossibleWords;
    }
}