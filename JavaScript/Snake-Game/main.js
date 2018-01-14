// IN PROGRESS
'use strict'
const SNAKE_SIZE = 20
const SNAKE_SPEED = 10

domready(() => {
  let canvas = document.getElementById('canvas')
  let ctx = canvas.getContext('2d')
  canvas.width = 700
  canvas.height = 500

  const rect = (drawColor, x, y, width, height) => {
    ctx.fillStyle = drawColor
    ctx.fillRect(x, y, width, height)
  }

  const random = (max) => {
    return Math.floor((Math.random() * max) + 1)
  }

  const getDist = (x1, x2, y1, y2) => {
    // Pythagorean Theorem
    let xDist = x1 - x2
    let yDist = y1 - y2
    return Math.sqrt(xDist * xDist + yDist * yDist)
  }

  /* ENGINE: */
  const FRAMES_PER_SECOND = 20
  setInterval(() => {
    render()
  }, 1000 / FRAMES_PER_SECOND)
  /* End of ENGINE: */

  /* SNAKE DIRECTION CONTROLER: */
  window.addEventListener('keydown', (e) => {
    switch (e.keyCode) {
      // up
      case 38:
      case 87:
        snake.direction(0, -SNAKE_SPEED)
        break
      // right
      case 39:
      case 68:
        snake.direction(SNAKE_SPEED, 0)
        break
      // down
      case 40:
      case 83:
        snake.direction(0, SNAKE_SPEED)
        break
      // left
      case 37:
      case 65:
        snake.direction(-SNAKE_SPEED, 0)
        break
    }
  })
  /* End of SNAKE DIRECTION CONTROLER: */

  class Snake {
    constructor () {
      this.x = 400
      this.y = 300
      this.xSpeed = SNAKE_SPEED
      this.ySpeed = 0

      this.total = 0
      this.tailX = []
      this.tailY = []

      this.direction = (x, y) => {
        this.xSpeed = x
        this.ySpeed = y
      }

      this.eat = () => {
        if (getDist(this.x, food.x, this.y, food.y) < SNAKE_SIZE - 2) {
          this.total++
          this.tailX.push(this.x)
          this.tailY.push(this.y)
          return true
        } else {
          return false
        }
      }

      this.update = () => {
        for (let i = 0; i < this.total; i++) {
          this.tailX[i] = this.tailX[i + 1]
        }
        this.x += this.xSpeed
        this.y += this.ySpeed
      }

      this.show = () => {
        // Draw the head:
        rect('white', this.x, this.y, SNAKE_SIZE, SNAKE_SIZE)
        // Draw the tail:
        for (let i = 0; i < this.total; i++) {
          rect('white', this.tailX[i], this.y, SNAKE_SIZE, SNAKE_SIZE)
        }
      }
    }
  }

  class Food {
    constructor (x, y) {
      this.x = x
      this.y = y

      this.show = () => {
        rect('green', this.x, this.y, SNAKE_SIZE, SNAKE_SIZE)
      }
    }
  }

  /* RENDER Function: */
  let snake = new Snake()
  let food = new Food(random(canvas.width), random(canvas.height))

  const render = () => {
    // Canvas:
    rect('black', 0, 0, canvas.width, canvas.height)

    // Snake:
    snake.update()
    snake.show()

    // Food:
    food.show()

    // New Food at eat() event:
    if (snake.eat()) {
      food = new Food(random(canvas.width), random(canvas.height))
    }
  }
  /* End of RENDER Function: */
})
