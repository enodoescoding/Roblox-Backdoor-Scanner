![Screenshot 2025-02-21 235216](https://github.com/user-attachments/assets/e9aecd9e-7254-490a-b353-255d833bd788)


# Roblox Model Scanner

A tool for scanning and identifying potential issues in Roblox models by fetching data from the Roblox API. This scanner helps to detect whether certain scripts in models contain potentially malicious code, such as the usage of require.

# Features
- Fetches models from Roblox's catalog based on a search term.
- Scans models to identify scripts with require statements, which could indicate the use of external resources or potentially malicious code.
- Outputs the model ID, script name, and line number where require is used.
- Provides a warning if suspicious code is detected, such as MaterialService, JointsService, or require.

# How It Was Made

1. Programming Language: C#

2. Libraries Used: HttpClient for making API requests to fetch model data from Roblox.
   Newtonsoft.Json for deserializing JSON data from the Roblox API.

3. API Used: The tool fetches data from the Roblox catalog via their public API (e.g., https://catalog.roblox.com/v1/search/items/details).

4.  Logic: Get Model Data: It fetches model data based on a search term.
Script Scanning: After fetching the models, it loads each model's scripts and checks for the usage of require.
Warning Detection: If certain keywords are found in the scripts, it marks the model as potentially dangerous.

5. Error Handling: Handles common errors such as request failures and invalid responses.
# How To Use
1. Clone the Repository: git clone https://github.com/yourusername/RobloxModelScanner.git
cd RobloxModelScanner

2. Install Dependencies: Make sure to install the necessary packages: Newtonsoft.Json for JSON parsing. You can install this via NuGet or use the command:
**dotnet add package Newtonsoft.Json**

3. Run the Program: Compile and run the program using your preferred IDE (e.g., Visual Studio) or via the .NET CLI:
**dotnet run**
Enter a search term (e.g., "hat") when prompted to fetch models from Roblox.

# Known Issues
API Limitations: The tool might not always fetch data properly due to API issues, rate-limiting, or changes in the API endpoints.
Model Availability: The model ID provided might not always return valid data. Some models might not be accessible or have restricted permissions.
API Changes: If Roblox changes the structure of their API or endpoints, this tool might break and require adjustments to the request logic.
External Dependencies: The script scanning relies on the structure of Roblox models and might not account for all possible variations in how models are built or scripts are written.

# Potential Future Improvements
Handle rate-limiting by implementing retries or delays between requests.
Add more advanced scanning to detect other potentially harmful behaviors in scripts.
Provide a UI to make it easier to use for non-developers.
