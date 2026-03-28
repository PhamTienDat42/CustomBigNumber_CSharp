!!! Use for Unity project.
!!! If you want to use in C# you need to change Debug.Log to Console.Log or something else available on your language and enviroment of project.
CustomBigNumber - Managing Large Numbers in C# for Idle & Clicker Games
📌 CustomBigValue is a C# library designed for handling extremely large numbers in games, particularly useful for Idle, Clicker, RPG, and financial simulation games. It supports arithmetic operations with exponent notation (E notation), optimizing performance when dealing with massive numbers.

✨ Key Features
✔️ Big Number Support: Works with numbers beyond standard double limits using exponent-based storage (E notation).
✔️ Familiar Operators: Supports arithmetic operations like +, -, *, /, and comparison operators ==, >, <, >=, <=.
✔️ Easy Integration: Plug-and-play support for Unity or any C# application.
✔️ Data Conversion: Easily convert between int, float, double, and string.
✔️ User-Friendly Formatting: Displays numbers in 1.23K, 5.6M, 9.87B, or 1.5E9 for better readability.

📌 Usage Example
A simple example:

csharp
Copy
Edit
CustomBigValue value1 = new CustomBigValue(1.5, 3); // 1.5E3 (1,500)
CustomBigValue value2 = new CustomBigValue(2.5, 6); // 2.5E6 (2,500,000)
CustomBigValue result = value1 * value2; // 3.75E9 (3,750,000,000)

Debug.Log(result.ToString()); // "3.75E9"
🔗 GitHub Repository
👉 CustomBigValue_CSharp

Let me know if you want any modifications! 🚀CustomBigValue - Managing Large Numbers in C# for Idle & Clicker Games
📌 CustomBigValue is a C# library designed for handling extremely large numbers in games, particularly useful for Idle, Clicker, RPG, and financial simulation games. It supports arithmetic operations with exponent notation (E notation), optimizing performance when dealing with massive numbers.

✨ Key Features
✔️ Big Number Support: Works with numbers beyond standard double limits using exponent-based storage (E notation).
✔️ Familiar Operators: Supports arithmetic operations like +, -, *, /, and comparison operators ==, >, <, >=, <=.
✔️ Easy Integration: Plug-and-play support for Unity or any C# application.
✔️ Data Conversion: Easily convert between int, float, double, and string.
✔️ User-Friendly Formatting: Displays numbers in 1.23K, 5.6M, 9.87B, or 1.5E9 for better readability.

📌 Usage Example
A simple example:

csharp
Copy
Edit
CustomBigValue value1 = new CustomBigValue(1.5, 3); // 1.5E3 (1,500)
CustomBigValue value2 = new CustomBigValue(2.5, 6); // 2.5E6 (2,500,000)
CustomBigValue result = value1 * value2; // 3.75E9 (3,750,000,000)

#Update 1
New method ToVisualString() for game developer.
When u call this method it's will return string with "K, M, B, T, ...." like 3k7 (3700), 5m3(5300000), 4b6(4600000000),....

Debug.Log(result.ToString()); // "3.75E9"
🔗 GitHub Repository
👉 CustomBigValue_CSharp

Let me know if you want any modifications! 🚀
