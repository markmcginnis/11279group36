# 11279group36

Member1: Mark McGinnis

Member2: Michael McGaha

Member3: Faizan Mohammad

Member4: Daniel Park

README.md
The local downloaded build of this project is the fully functional and optimized build of this game. Features like the IBM text to speech options are not available on the WebGL builds of our software and the WebGL build does not properly render all UI elements.

After downloading FisheryManagerSimulator.zip, unzip it into the folder of your choice, then simply run the FisheryManager.exe file contained within.

NOTE: The unzipping portion is very important because otherwise the program will not run correctly or at all. 

NOTE: In order to work in this project you must be using version 2021.1.20f1 of the unity editor. Other editor versions may not be compatible with all software and can cause build issues.

The WebGL build is accessible through the link on the hosting website:
https://simmer.io/@L0k1/fishery-manager-simulator 


FisheryManager.cs
The FisheryManager is the main script of the simulation and utilizes the GameManager and EconomyManager scripts to perform and store the math controlling the simulation. Parameters pertaining to fish population, fecal matter concentration, and player-input sliders are contained within this script.

Relevant Functions
public void TimeStep()
The function takes no arguments and returns nothing.
The function controls when the simulation moves time forward within the simulation. It first increments the week count and then makes four function calls to Harvest(), AdvanceFecal(), Disease(), and Birth() defined below.

public void Harvest()
The function takes no arguments and returns nothing.
The function is dependent on a public boolean that represents when harvest mode is off (when the harvest button is marked “Harvest: No”) or when harvest mode is on (when the harvest button is marked “Harvest: Yes”). 
When harvest mode is off, the function calls EconomyManager’s Sell function with zero fish sold.
When harvest mode is on, the function calls EconomyManager’s Sell function with the portion of fish population being sold based on the harvest slider.
(See ToggleHarvest() function below, pg. 13)
(Refer to pg. 13 for EconomyManager functions)

public void ToggleHarvest()
The function takes no argument and returns nothing.
The function toggles harvest mode when the harvest button is pressed. Harvest mode is off by default.

public void AdvanceFecal()
The function takes no arguments and returns nothing.
The function calls the GetFecalConcentration function within the GameManager to update the current fecal matter concentration. The value returned is clamped between 0 and max fecal matter concentration.
(Refer to pg. 14 for GameManager functions)

public void Disease()
The function takes no arguments and returns nothing.
The function calls the Disease and Recovery function within the GameManager to update the current diseased population and healthy population.
(Refer to pg. 14 for GameManager functions)

public void Birth()
The function takes no arguments and returns nothing.
The function calls the EulerLogistic function within the GameManager to update the current population size and healthy population.
(Refer to pg. 14 for GameManager functions)

public void DecreaseGrowth()
The function takes no arguments and returns nothing.
The function lowers the current growth constant to a lower limit of 0.2. This function is called by the decrease feeding button on the MainScene.

public void IncreaseGrowth()
The function takes no arguments and returns nothing.
The function lowers the current growth constant to an upper limit of 0.8. This function is called by the increase feeding button on the MainScene.

EconomyManager.cs
The EconomyManager maintains the parameters relevant to the economy tab within the MainScene like revenue, profits, costs, fish selling price, and amount of fish sold.

Relevant Functions
public void Sell(int harvestCount, float filtering, float antibiotics)
Arguments: portion of fish sold, the filtering slider value, and antibiotics slider value.
Updates total revenue based on the harvest amount and cost of filtering and antibiotics and keeps track of total fish sold through fishery’s lifespan.

GameManager.cs
The GameManager acts as a helper and controller for the other main scripts, Fishery and Economy, and contains references for some of the visual user interface elements, like meters and text, on the MainScene. In addition, GameManager contains the math functions used to calculate changes to the fish population.

Relevant Functions
public int EulerLogistic(int currentPop, int carryingCap, float n, float h, float growthConstant)
Arguments: Current fish population, population’s carrying capacity, Euler’s method window and step size, and the growth constant determined by the player’s feeding amount.
The function returns a single integer representing the fish population after a change in time using Euler’s Method and a logistic ordinary differential equation 
(Refer to the TimeStep() function within the FisheryManager on pg. 12).

public float Disease(int population, float maxConc, float fecalMatterConc, float antibiotics, float antibioticsMax)
Arguments: Current fish population, maximum fecal matter concentration, current fecal matter concentration, current antibiotic use, and maximum antibiotic use.
The function returns the probability of fish becoming diseased.

public float Recovery(int population, float maxConc, float fecalMatterConc, float antibiotics, float antibioticsMax)
Arguments: Current sick fish population, maximum fecal matter concentration, current fecal matter concentration, current antibiotic use, and maximum antibiotic use.
The function returns the probability of fish recovering from disease.

public float GetFecalConcentration(int population, float fecalMatterConc, float decomposition)
Arguments: the current population, current fecal matter concentration, and decomposition (current filtering value multiplied by the decomposition rate)
Calculates and returns the fecal matter concentration for AdvanceFecal(), pg. 13, based on current simulation parameters.

public void saveGame()
The function takes no arguments and returns nothing.
This function calls the saveData function in the SaveAndLoad script using the data currently in the fishery tab, economy tab, and line graphing data.
(Refer to SaveAndLoad script, pg.15-16)

public void loadGame()
The function takes no arguments and returns nothing.
This function creates a new save data object with the loaded data from the save file, and it writes that information to the public variables in the fishery tab, economy tab, and the line graph.

LineGraphManager.cs
This script holds the data for the graph in the Economy tab. In addition, it holds the references to all GameObjects that draw and define the bounds of the graph.

Relevant Functions
public void AddPlayer1Data()
The function takes no arguments and returns nothing.
This function creates a new GraphData object with data equal to the current population divided by the carrying capacity as a percentage and adds the object to the list containing population data points.

public void AddPlayer2Data()
The function takes no arguments and returns nothing.
This function creates a new GraphData object with data equal to the current total profit in thousands of dollars and adds the object to the list containing fishery profit data points.

public void ShiftLeftDataX()
The function takes no arguments and returns nothing.
Where X represents 1 or 2 referring to graphData1 or graphData2.
Since the graph plots the ten most recent data points, the list is shifted left to remove the first data point when adding more data points and the list has ten data points.

public void ShowGraph()
The function takes no arguments and returns nothing.
Utilizes other helper functions to draw and plot the data points onto the graph. That is to say, if the data lists are empty, the graph will plot zero points.

public void ClearLists()
The function takes no arguments and returns nothing.
This function empties both population and profit data lists effectively removing all current data points from the graph.

SaveAndLoad.cs
This function handles saving and loading the data in the fishery simulator using a binary formatter to encrypt and decrypt the user’s current data.

Relevant Functions
public static void saveGame(FisheryManager fisheryData, EconomyManager economyData, LineGraphManager graphData)
This function takes in the fishery data, economy data, and line data from their respective scripts as parameters. This function does not return anything. It creates a file in a valid destination on the user’s device and creates a save data object from the saveData.cs script (see SaveData function in SaveData.cs). From here the data is written to a save file, converted to binary for encryption, and the file is closed.

public static SaveData loadGame()
This function takes in no parameters. It checks for a save file in the saved location, and if located, decrypts it as a save file and returns the saved data. If a save file is not located on the user’s device, this function returns null.

SaveData.cs
This script creates the save data class that includes variables for all the relevant data in the fishery manager, economy manager, and graphing tool.

Relevant Functions
public SaveData(FisheryManager fisheryData, EconomyManager economyData, LineGraphManager graphData)
This function takes in three parameters, the fishery data, the economy data, and the graph data. It then sets the values of the variables in the saveData class equal to the values currently in the simulation. This function does not return anything and simply constructs the object.

UserGuide.cs
The user guide creates a call to the IBM text to speech API and outputs the inputted text orally to the user.
Relevant Functions
private IEnumerator CreateService()
This function creates the ibm text to speech service. If a valid api key and link is provided, the service is created.

private IEnumerator ExampleSynthesize(string text)
This function uses the inputted text to create a service call using the inputted text as the text to translate. The service can be modified by changing the voice using the IBM documentation (see IBM resources), the input text, or the file type. 

private void PlayClip(AudioClip clip)
This function plays the created text to speech audio using the selected inputs and is called in example synthesize. It destroys the clip at the end and returns nothing.

MainMenu.cs
This script provides functions for when the 3 menu options (Start, quit, or guide) are selected and calls the corresponding scene or closes the game application.



IBM Text to Speech API Resources
For resources on how to modify IBM text to speech content such as voices, calls, or to request API keys, please use the following links to resources.
Text to speech documentation: https://cloud.ibm.com/apidocs/text-to-speech?code=unity
Watson Developer Cloud for Unity: https://github.com/watson-developer-cloud/unity-sdk
IBM SDK Core for Unity: https://github.com/IBM/unity-sdk-core
Sign up for API key: https://www.ibm.com/cloud/watson-text-to-speech?utm_content=SRCWW&p1=Search&p4=43700057522849802&p5=b&gclid=EAIaIQobChMIn9270IGj9AIVyYpaBR2DTAnYEAAYASAAEgLnuvD_BwE&gclsrc=aw.ds

In order to obtain an API key, please select “create an IBM cloud account” in the top right corner of the website, and following this go back to the IBM text to speech API page at the link. Select the tab “start for free” and then select the lite plan. Input the closest region to your location when selecting a region for your url, and then create your plan. You will now have an API key and url shown on your IBM watson account, as well as the number of calls you are permitted per month. The limit on the lite plan is 10000 calls per month.
