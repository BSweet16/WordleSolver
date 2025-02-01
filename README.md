# Wordle Solver

A C# application designed to solve Wordle puzzles efficiently by narrowing down possible words based on user feedback. This program uses a comprehensive list of 5-letter English words and employs a smart algorithm to guess the target word in the fewest attempts possible.

---
![image](https://github.com/user-attachments/assets/1cd912fe-3f94-4cf8-9a9c-09eefcc79826)

## Example Output
```
Welcome to the Wordle Solver!
Word list loaded successfully.
Attempt #1
Guess: ALERT
Enter feedback (G for green, Y for yellow, X for gray):
XXXXX

Attempt #2
Guess: NOISY
Enter feedback (G for green, Y for yellow, X for gray):
YXXYG

Attempt #3
Guess: SUNNY
Enter feedback (G for green, Y for yellow, X for gray):
GGGGG

Congratulations! You solved the Wordle in 3 attempts.
```


---

## Benefits of the Program

1. **Efficient Solving**:
   - The solver uses a smart algorithm to narrow down possible words based on user feedback, ensuring the target word is found in the fewest attempts.

2. **No API Dependency**:
   - The program uses a static list of 5-letter English words, eliminating the need for external APIs or internet connectivity.

3. **User-Friendly**:
   - The solver interacts with the user by prompting for feedback after each guess, making it easy to use even for non-technical users.

4. **Handles Complex Cases**:
   - The program correctly handles repeated letters and ambiguous feedback, ensuring accurate results.

5. **Educational**:
   - By observing how the solver works, users can learn strategies for solving Wordle puzzles more effectively.

---

## How It Works

### 1. **Word List**
   - The program uses a static list of over 1,000 common 5-letter English words stored in the `WordList.cs` file. This list is used to generate guesses and narrow down possibilities.

### 2. **Feedback-Based Guessing**
   - After each guess, the user provides feedback in the form of a 5-character string (`G`, `Y`, or `X`):
     - `G`: Correct letter in the correct position (Green).
     - `Y`: Correct letter in the wrong position (Yellow).
     - `X`: Incorrect letter (Gray).

### 3. **Narrowing Down Words**
   - The program uses the feedback to eliminate impossible words from the list:
     - **Green Feedback**: Ensures the letter is in the correct position.
     - **Yellow Feedback**: Ensures the letter exists in the word but not in the guessed position.
     - **Gray Feedback**: Ensures the letter does not exist in the word.
     - **Repeated Letters**: Handles cases where a letter appears multiple times in the guessed word.

### 4. **Optimal Guessing**
   - The solver prioritizes words with the most common letters to maximize the chances of getting useful feedback.
   - Starts with the optimal starting word based on the following information.

   To optimize the starting word for the Wordle solver, we need to choose a word that provides the maximum information about the target word. This means selecting a word that:
   1. Contains the most common letters in the English language.
   2. Maximizes the number of unique letters to increase the chances of getting useful feedback.
   3. Balances vowel and consonant distribution to cover a wide range of possibilities.

    **Strategy for Choosing the Starting Word:**
    1. Letter Frequency Analysis:
        * Use a list of the most common letters in 5-letter English words.
        * Prioritize words that contain these letters.
    2. Unique Letters:
        * Choose a word with as many unique letters as possible to maximize the chances of getting feedback for different letters.
    3. Vowel Coverage:
        * Ensure the starting word contains multiple vowels, as most English words contain vowels.

    **Most Common Letters in 5-Letter Words:**
    Based on frequency analysis, the most common letters in 5-letter English words are:

    ```
    E, A, R, O, T, L, I, S, N, C, U, D, H, M, P, Y, G, B, K, F, W, V, Z, X, J, Q
    ```

    **Implementation:**
    To implement this optimization, we can:
        1. Precompute the best starting word based on letter frequency and uniqueness.
        2. Use this word as the first guess in the solver.


### 5. **Winning the Game**
   - The program continues guessing until the target word is found or the maximum number of attempts (6) is reached.

---

## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0 or higher).

### Libraries
- Newtonsoft.Json (v13.0)


### Installation
1. Clone the repository:
- git clone https://github.com/BSweet16/WordleSolver.git
2. Navigate to the project directory:
- cd WordleSolver
3. Build and Run to Enjoy!

### License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/BSweet16/WordleSolver/blob/main/LICENSE) file for details.

### Acknowledgments
* Inspired by the popular game Wordle.
* Exploring the new AI [Deep Seek](https://chat.deepseek.com/) to create this functionality. 
