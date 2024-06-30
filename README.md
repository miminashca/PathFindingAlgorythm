# PathFinding Algorithm

## Description

**PathFinding Algorithm** is a project that demonstrates various pathfinding algorithms implemented in C#. This project was designed to illustrate the functionality and efficiency of different pathfinding techniques. The algorithms are applied within a graphical environment, providing visual feedback on their performance and results.

### Background
This project was developed to deepen the understanding of pathfinding algorithms, which are essential in various applications such as robotics, video games, and network routing. The project includes multiple algorithms, each designed to find the optimal path between two points in a grid-based environment.

### Key Features
- **Visual Representation**: The project provides a visual representation of the pathfinding process, allowing users to see the algorithms in action.
- **Multiple Algorithms**: Various pathfinding algorithms are implemented, including A*, Dijkstra's, and Breadth-First Search.
- **User Interaction**: Users can interact with the program to set start and end points and observe how different algorithms perform.

## Project Structure

### Root Directory
- **PathFindingAlgorythm/**: Contains the main project directory.
  - **.DS_Store**: macOS file for folder attributes (can be ignored or deleted).
  - **README.md**: Project documentation file.
  - **GXPEngine/**: Contains the source code and related files for the game engine.
  - **sources/**: Contains the source code files for the project.
    - **.DS_Store**: macOS file for folder attributes (can be ignored or deleted).
    - **Util/**: Contains utility classes and methods.
      - **NodeLabelDrawer.cs**: Utility for drawing node labels.
      - **Grid.cs**: Grid class for the environment.
      - **Vec2.cs**: Vector mathematics utility.
      - **RectangleExtensions.cs**: Extension methods for rectangle operations.
    - **AlgorithmsAssignment.cs**: Main file for the pathfinding algorithms assignment.
    - **Assignment/**: Contains additional assignment-specific code.
    - **Solution/**: Contains solution-specific code.
    - **Program.cs**: Main program file that initializes and runs the pathfinding algorithms.
  - **.git/**: Git repository containing version control information.

## Getting Started

### Prerequisites
- Visual Studio or a compatible C# development environment.

### Building the Project
1. Clone the repository or download the project files.
2. Open the solution file in Visual Studio.
3. Build the solution using Visual Studio (Build > Build Solution).

### Running the Project
- After building the solution, run the executable to start the program.
- Use the graphical interface to set start and end points and select different algorithms to see how they perform.
