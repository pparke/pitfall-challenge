# Requirements

## Player Abilities
- [x] ability to run left and right
- [x] jump
- [x] take damage
- [x] die

## Environment
- [x] contains upper and lower level
- [x] 1 long scene (camera moves smoothly with player character)

## Enemy/obstacle type per screen
- [x] holes to jump over
  - [x] player falls to lower level
  - [x] player falls off-screen into pit (death)
- [x] barrel
  - [x] spawns continually
  - [x] rolls across screen
  - [x] causes player damage on touch
- [x] crocodile
  - crocodiles have two states
    - [x] mouth closed (safe to jump on)
    - [x] mouth open (fatal to player)
- [x] swinging vine
  - [x] vine swings left and right
  - [x] on colliding with vine, player character becomes attached
  - [x] on pressing jump, player character detaches
- [x] lives
  - [x] player given 3 lives
  - [x] must decrement a life each time they die
  - [x] game restarts on death


# Folder Structure
- **Assets**
  - Animation
    - Behaviours
    - Clips
    - Controllers
  - Audio
    - Enemy
    - FX
    - Music
    - Player
  - Fonts
  - Materials
  - Physics Materials
  - Prefabs
    - Characters
    - Environment
	- FX
    - Obstacles
    - Props
    - UI
 - Scenes
 - Scripts
   - Editor
   - Interfaces
   - Player
 - Shaders
 - Sprites
   - Characters
   - Effects
   - Environment
   - Obstacles
   - Props
   - UI

# Class List
![PlantUML model](http://plantuml.com/plantuml/png/3SJ94O0m2030LhG0mzzk4D5a90d3CRQl-zYr3P8yEKsM6g6-0nY-vMvHyqXdepc2HnIQ7LJH7WPjSQ78HlMVnytGqOCDpab0WkxIF8fcbmy0)
