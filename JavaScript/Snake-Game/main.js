// IN PROGRESS
'use strict'
domready(() => {
  let player = {
    name: '',
    bestScore: 0
  }

  const SNAKE_SIZE = 10
  const START_SPEED = 10
  let snakeSpeed = START_SPEED

  // INIT:
  let canvas = document.getElementById('canvas')
  let ctx = canvas.getContext('2d')
  ctx.scale(2, 2)
  canvas.width = 500
  canvas.height = canvas.width

  // UTILITY FUNCTIONS:
  const rect = (drawColor, x, y, width, height) => {
    ctx.fillStyle = drawColor
    ctx.fillRect(x, y, width, height)
  }

  const random = (canvasSize, gridSize) => {
    return Math.floor(Math.random() * canvasSize / gridSize) * gridSize
  }

  const dist = (x1, x2, y1, y2) => {
    // Pythagorean Theorem
    let xDist = x1 - x2
    let yDist = y1 - y2
    return Math.sqrt(xDist * xDist + yDist * yDist)
  }

  /* SNAKE DIRECTION CONTROLER: */
  window.addEventListener('keydown', (e) => {
    switch (e.keyCode) {
      // UP:
      case 38:
      case 87:
        // Don't let player turn the contrary direction:
        if (snake.ySpeed === snakeSpeed) {
          return
        }
        snake.direction(0, -snakeSpeed)
        break
      // RIGTH:
      case 39:
      case 68:
        if (snake.xSpeed === -snakeSpeed) {
          return
        }
        snake.direction(snakeSpeed, 0)
        break
      // DOWN:
      case 40:
      case 83:
        if (snake.ySpeed === -snakeSpeed) {
          return
        }
        snake.direction(0, snakeSpeed)
        break
      // LEFT:
      case 37:
      case 65:
        if (snake.xSpeed === snakeSpeed) {
          return
        }
        snake.direction(-snakeSpeed, 0)
        break
    }
  })
  /* End of SNAKE DIRECTION CONTROLER: */

  const snakeAIController = (snakeAI, food) => {
    if (food.x > snakeAI.x) {
      if (snakeAI.xSpeed === -SNAKE_SIZE && snake.y >= canvas.height * 0.95) {
        snakeAI.direction(0, -SNAKE_SIZE)
      } else if (snakeAI.xSpeed === -SNAKE_SIZE && snake.y < canvas.height * 0.95) {
        snakeAI.direction(0, SNAKE_SIZE)
      }
      snakeAI.direction(SNAKE_SIZE, 0)
    }

    if (food.x < snakeAI.x) {
      if (snakeAI.xSpeed === SNAKE_SIZE && snake.y >= canvas.height * 0.95) {
        snakeAI.direction(0, -SNAKE_SIZE)
      } else if (snakeAI.xSpeed === SNAKE_SIZE && snake.y < canvas.height * 0.95) {
        snakeAI.direction(0, SNAKE_SIZE)
      }
      snakeAI.direction(-SNAKE_SIZE, 0)
    }

    if (food.x === snakeAI.x && food.y > snakeAI.y) {
      if (snakeAI.ySpeed === -SNAKE_SIZE && snake.y >= canvas.width * 0.95) {
        snakeAI.direction(-SNAKE_SIZE, 0)
      } else if (snakeAI.ySpeed === -SNAKE_SIZE && snake.y < canvas.width * 0.95) {
        snakeAI.direction(SNAKE_SIZE, 0)
      }
      snakeAI.direction(0, SNAKE_SIZE)
    }

    if (food.x === snakeAI.x && food.y < snakeAI.y) {
      if (snakeAI.ySpeed === SNAKE_SIZE && snake.y >= canvas.width * 0.95) {
        snakeAI.direction(-SNAKE_SIZE, 0)
      } else if (snakeAI.ySpeed === SNAKE_SIZE && snake.y < canvas.width * 0.95) {
        snakeAI.direction(SNAKE_SIZE, 0)
      }
      snakeAI.direction(0, -SNAKE_SIZE)
    }
  }

  class Snake {
    constructor (type) {
      this.type = type
      this.x = (canvas.width / 2) - (SNAKE_SIZE / 2)
      this.y = (canvas.height / 2) - (SNAKE_SIZE / 2)
      this.xSpeed = 0
      this.ySpeed = 0
      this.tail = []
      this.respawn = false

      this.direction = (x, y) => {
        this.xSpeed = x
        this.ySpeed = y
      }

      this.eat = () => {
        if (dist(this.x, food.x, this.y, food.y) < SNAKE_SIZE) {
          return true
        }
        return false
      }

      this.death = () => {
        // Canvas edjes:
        if (this.x < 0 || this.x > canvas.width - SNAKE_SIZE) {
          return true
        } else if (this.y < 0 || this.y > canvas.height - SNAKE_SIZE) {
          return true
        }
        // Snake tail:
        for (let i = 0; i < this.tail.length; i++) {
          if (dist(this.x, this.tail[i].x, this.y, this.tail[i].y) < 1) {
            return true
          }
        }
        return false
      }

      this.update = () => {
        // Shift the x and y positions of each part of the tail by the next one in line:
        if (this.tail.length >= 1) {
          for (let i = 0; i < this.tail.length - 1; i++) {
            this.tail[i] = this.tail[i + 1]
          }
          // The last tail part indexes are the last indexes of the head:
          this.tail[this.tail.length - 1] = {
            x: this.x,
            y: this.y
          }
        }
        // Move position of the snake:
        this.x += this.xSpeed
        this.y += this.ySpeed
      }

      this.respawn = () => {
        snake = new Snake()
      }

      this.show = (color) => {
        // Draw the head:
        rect(color, this.x, this.y, SNAKE_SIZE, SNAKE_SIZE)
        // Draw the tail:
        for (let i = 0; i < this.tail.length; i++) {
          rect(color, this.tail[i].x, this.tail[i].y, SNAKE_SIZE, SNAKE_SIZE)
        }
      }
    }
  }

  class Food {
    constructor () {
      this.x = random(canvas.width - SNAKE_SIZE, SNAKE_SIZE)
      this.y = random(canvas.width - SNAKE_SIZE, SNAKE_SIZE)

      this.respawn = () => {
        console.log('respawn')
        food = new Food()
      }

      this.show = () => {
        rect('green', this.x, this.y, SNAKE_SIZE, SNAKE_SIZE)
      }
    }
  }

  let snake = new Snake('Player')

  let snakeAI = new Snake('AI')
  snakeAI.x = random(canvas.width - SNAKE_SIZE, SNAKE_SIZE)
  snakeAI.y = random(canvas.width - SNAKE_SIZE, SNAKE_SIZE)

  let food = new Food()

  /* GAME RULES: */
  const gameRules = (snake) => {
    // Snake.death() event:
    if (snake.death() && snake.type === 'Player') {
      document.getElementById('score').innerHTML = 0
      if (snake.tail.length > player.bestScore) {
        player.bestScore = snake.tail.length
        document.getElementById('bestScore').innerHTML = player.bestScore
      }
      food.respawn()
      snake.respawn()
      snakeSpeed = START_SPEED
    }

    if (snakeAI.death()) {
      console.log('enemy dyed')
      snakeAI = new Snake()
    }

    // Snake.eat() event:
    if (snake.eat()) {
      // The food (last snake head coordinates) goes into the tail:
      snake.tail.push({
        x: this.x,
        y: this.y
      })

      food.respawn()
      if (snake.type === 'Player') {
        snakeSpeed += 0.2
        const scoreShow = document.getElementById('score')
        scoreShow.innerHTML = parseInt(scoreShow.innerHTML) + 1
      }
    }
  }

  /* RENDER: */

  const render = () => {
    // Canvas:
    rect('black', 0, 0, canvas.width, canvas.height)

    // Snake:
    snake.update()
    snake.show('white')

    
    // Food:
    food.show()
    
    // AI Snake:
    snakeAIController(snakeAI, food)
    snakeAI.update()
    snakeAI.show('red')

    // Game rules:
    gameRules(snakeAI)
    gameRules(snake)
  }
  /* End of RENDER. */

  /* ENGINE: */
  const FRAMES_PER_SECOND = 15
  setInterval(() => {
    render()
  }, 1000 / FRAMES_PER_SECOND)
  /* End of ENGINE. */
})
