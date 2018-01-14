let canvas
let ctx

const WINNING_SCORE = 3
let leftPlayerScore = 0
let computerScore = 0

let showingWinScreen = false

const BALL_RADIUS = 5
let ballX = 15
let ballY = 50
let ballSpeedX = 5
let ballSpeedY = 4

const STICK_HEIGHT = 100
const STICK_WIDTH = 10
const LEFT_STICK_DISTANCE = 15
let leftStickY = 250
let rigthStickY = 250

const COMPUTER_SPEED_Y = 4.9

$('document').ready(() => {
  // Canvas init:
  canvas = document.getElementById('canvas')
  ctx = canvas.getContext('2d')
  canvas.width = 800
  canvas.height = 600

  canvas.addEventListener('click', () => {
    if (showingWinScreen) {
      leftPlayerScore = 0
      computerScore = 0
      showingWinScreen = false
    }
  })

  // Mouse Move event listener:
  canvas.addEventListener('mousemove', (e) => {
    let mousePosition = getMousePosition(e)
    leftStickY = mousePosition.y - STICK_HEIGHT / 2
  })

  // Engine:
  const FRAMES_PER_SECOND = 35
  setInterval(() => {
    movementLogic()
    drawEverything()
  }, 1000 / FRAMES_PER_SECOND)
})

const getMousePosition = (e) => {
  let rect = canvas.getBoundingClientRect()
  let root = document.documentElement
  let mouseX = e.clientX - rect.left - root.scrollLeft
  let mouseY = e.clientY - rect.top - root.scrollTop
  return {
    x: mouseX,
    y: mouseY
  }
}

const resetBall = () => {
  if (leftPlayerScore >= WINNING_SCORE || computerScore >= WINNING_SCORE) {
    showingWinScreen = true
  }
  // Flip X direction of the ball:
  ballSpeedX = -ballSpeedX
  // Center the ball:
  ballX = canvas.width / 2
  ballY = canvas.height / 2
}

const computerMovement = () => {
  let rightStickYCenter = rigthStickY + STICK_HEIGHT / 2
  if (rightStickYCenter < ballY - 35) {
    rigthStickY += COMPUTER_SPEED_Y
  }

  if (rigthStickY > ballY - 35) {
    rigthStickY -= COMPUTER_SPEED_Y
  }
}

const movementLogic = () => {
  if (showingWinScreen) {
    return
  }
  /* Computer: */
  computerMovement()

  /* Ball: */
  ballX += ballSpeedX
  ballY += ballSpeedY
  // Horizontal:
  if (ballX < 0) {
    if (ballY > leftStickY && ballY < leftStickY + STICK_HEIGHT) {
      ballSpeedX = -ballSpeedX
      // When deltaY=0, the ball is in the center of the stick.
      let deltaY = ballY - (leftStickY + STICK_HEIGHT / 2)
      ballSpeedY = deltaY * 0.25
    } else {
      computerScore++
      resetBall()
    }
  }
  if (ballX > canvas.width) {
    if (ballY > rigthStickY && ballY < rigthStickY + STICK_HEIGHT) {
      ballSpeedX = -ballSpeedX
      // When deltaY=0, the ball is in the center of the stick.
      let deltaY = ballY - (rigthStickY + STICK_HEIGHT / 2)
      ballSpeedY = deltaY * 0.35
    } else {
      leftPlayerScore++
      resetBall()
    }
  }
  // Vertical:
  if (ballY < 0) {
    ballSpeedY = -ballSpeedY
  }
  if (ballY > canvas.height) {
    ballSpeedY = -ballSpeedY
  }
}

const drawNet = () => {
  for (let i = 0; i < canvas.height; i++) {}
}

const drawEverything = () => {
  // Clear content:
  ctx.clearRect(0, 0, canvas.width, canvas.height)

  // Canvas:
  drawRect('black', 0, 0, canvas.width, canvas.height)

  // Net dashed line:
  ctx.strokeStyle = 'white'
  ctx.lineWidth = 2
  ctx.setLineDash([20, 15])
  ctx.beginPath()
  ctx.moveTo(400, 0)
  ctx.lineTo(400, 600)
  ctx.stroke()

  if (showingWinScreen) {
    ctx.fillStyle = 'white'
    if (leftPlayerScore >= WINNING_SCORE) {
      ctx.fillText('Left player won!', 310, 300)
    } else if (computerScore >= WINNING_SCORE) {
      ctx.fillText('Computer won!', 310, 300)
    }
    ctx.fillText('Click to Continue', 300, 500)
    return
  }

  // Left Stick:
  drawRect('white', LEFT_STICK_DISTANCE, leftStickY, STICK_WIDTH, STICK_HEIGHT)

  // Right (COMPUTER) Stick:
  drawRect('white', canvas.width - LEFT_STICK_DISTANCE - STICK_WIDTH, rigthStickY, STICK_WIDTH, STICK_HEIGHT)

  // Ball:
  drawCircle('white', ballX, ballY, BALL_RADIUS)

  // Score:
  ctx.font = '30px Arial'
  ctx.fillText(leftPlayerScore, 100, 100)
  ctx.fillText(computerScore, canvas.width - 100, 100)
}

const drawRect = (drawColor, x, y, width, height) => {
  ctx.fillStyle = drawColor
  ctx.fillRect(x, y, width, height)
}

const drawCircle = (drawColor, centerX, centerY, radius) => {
  ctx.fillStyle = drawColor
  ctx.beginPath()
  ctx.arc(centerX, centerY, radius, 0, Math.PI * 2, true)
  ctx.fill()
}
