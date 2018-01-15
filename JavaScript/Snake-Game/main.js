// IN PROGRESS
'use strict'
domready(() => {
  const SNAKE_SIZE = 20
  const SNAKE_SPEED = 15

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
      this.tail = []

      this.direction = (x, y) => {
        this.xSpeed = x
        this.ySpeed = y
      }

      this.eat = () => {
        if (getDist(this.x, food.x, this.y, food.y) < SNAKE_SIZE - 2) {
          this.total++
          return true
        } else {
          return false
        }
      }

      this.update = () => {
        // Shift the x and y positions of each part of the tail by the next one in line:
        if (this.total >= 1) {
          for (let i = 0; i < this.tail.length - 1; i++) {
            this.tail[i] = this.tail[i + 1]
          }
          // The last tail part indexes are the last indexes of the head:
          this.tail[this.total - 1] = {
            x: this.x,
            y: this.y
          }
        }
        // Move the snake head:
        this.x += this.xSpeed
        this.y += this.ySpeed
      }

      this.show = () => {
        // Draw the head:
        rect('white', this.x, this.y, SNAKE_SIZE, SNAKE_SIZE)
        // Draw the tail:
        for (let i = 0; i < this.tail.length; i++) {
          rect('white', this.tail[i].x, this.tail[i].y, SNAKE_SIZE, SNAKE_SIZE)
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

    // New Food at snake.eat() event:
    if (snake.eat()) {
      food = new Food(random(canvas.width - SNAKE_SIZE), random(canvas.height - SNAKE_SIZE))
    }
  }
  /* End of RENDER Function: */

  /* ENGINE: */
  const FRAMES_PER_SECOND = 20
  setInterval(() => {
    render()
  }, 1000 / FRAMES_PER_SECOND)
  /* End of ENGINE: */
})
