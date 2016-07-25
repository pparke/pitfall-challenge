# Requirements

## Player Abilities
- [ ] ability to run left and right
- [ ] jump
- [ ] take damage
- [ ] die

## Environment
- [ ] contains upper and lower level
- [ ] 1 long scene (camera moves smoothly with player character)

## Enemy/obstacle type per screen
- [ ] holes to jump over
  - [ ] player falls to lower level
  - [ ] player falls off-screen into pit (death)
- [ ] barrel
  - [ ] spawns continually
  - [ ] rolls across screen
  - [ ] causes player damage on touch
- [ ] crocodile
  - crocodiles have two states
    - [ ] mouth closed (safe to jump on)
    - [ ] mouth open (fatal to player)
- [ ] swinging vine
  - [ ] vine swings left and right
  - [ ] on colliding with vine, player character becomes attached
  - [ ] on pressing jump, player character detaches
- [ ] player given 3 lives
  - [ ] must decrement a life each time they die
  - [ ] game restarts on death


# Folder Structure
- **Assets**
  - Animation
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
    - Props
    - UI
 - Scenes
 - Scripts
 - Sprites
   - Characters
   - Environment
   - FX
   - Props
   - UI

# Class List
![PlantUML model](http://plantuml.com/plantuml/png/3SJ94O0m2030LhG0mzzk4D5a90d3CRQl-zYr3P8yEKsM6g6-0nY-vMvHyqXdepc2HnIQ7LJH7WPjSQ78HlMVnytGqOCDpab0WkxIF8fcbmy0)
