import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';

function Square(props) {
    return (
        <button className="square"
                onClick={ props.onClick }
        >
            { props.value }
        </button>
    );
}

class Board extends React.Component {

    renderSquare(i) {
        return (
            <Square value={ this.props.squares[i] }
                    onClick={ () => this.props.onClick(i) }
            />
        );
    }

    render() {
        return (
            <div>
                <div className="board-row">
                    {this.renderSquare(0)}
                    {this.renderSquare(1)}
                    {this.renderSquare(2)}
                </div>
                <div className="board-row">
                    {this.renderSquare(3)}
                    {this.renderSquare(4)}
                    {this.renderSquare(5)}
                </div>
                <div className="board-row">
                    {this.renderSquare(6)}
                    {this.renderSquare(7)}
                    {this.renderSquare(8)}
                </div>
            </div>
        );
    }
}

class Game extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            history: [
                { squares: Array(9).fill(null) }
            ],
            moveNumber: 0,
            xIsNext: true
        };
    }

    render() {
        const currentGameState = this.____getCurrentGameState();
        const winner = calculateWinner(currentGameState.squares);

        const movesList = this.state.history.map((history, iMove) => {
            const label = iMove ?
                `Go to move #${iMove}` :
                `Go to the game start`;

            return (
                <li key={iMove}>
                    <button onClick={ () => this.jumpToMove(iMove) }>{label}</button>
                </li>
            );
        });

        let status;
        if (winner) {
            status = `Winner: ${winner}`;

        } else {
            status = `Next player: ${this.state.xIsNext ? 'X' : 'O'}`;
        }

        return (
            <div className="game">
                <div className="game-board">
                    <Board squares={ currentGameState.squares }
                           onClick={ (i) => this.handleSquareClick(i) }
                    />
                </div>
                <div className="game-info">
                <div> { status } </div>
                    <ol>{ movesList }</ol>
                </div>
            </div>
        );
    }

    handleSquareClick(i) {
        const currentGameState = this.____getCurrentGameState();
        const squares = currentGameState.squares.slice();

        if (calculateWinner(squares) || squares[i]) {
            return;
        }

        squares[i] = this.state.xIsNext ? 'X' : 'O';
        this.setState({
            history: this.state.history.concat([{ squares: squares }]),
            moveNumber: this.state.history.length,
            xIsNext: !this.state.xIsNext
        });
    }

    jumpToMove(iMove) {
        this.setState({
            history: this.state.history.slice(0, iMove + 1),
            moveNumber: iMove,
            xIsNext: isNumberEven(iMove)
        })
    }

    ____getCurrentGameState() {
        return this.state.history[this.state.moveNumber];
    }
}

// ========================================

ReactDOM.render(
    <Game />,
    document.getElementById('root')
);

function calculateWinner(squares) {
    const lines = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];

    for (let i = 0; i < lines.length; i++) {
        const [a, b, c] = lines[i];

        if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
            return squares[a];
        }
    }

    return null;
}

function isNumberEven(number) {
    return number % 2 === 0;
}
