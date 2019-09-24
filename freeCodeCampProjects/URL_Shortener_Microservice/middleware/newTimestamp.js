const newTimestamp = () => {
    const date = new Date().toISOString().substr(0, 10)
    const time = new Date().toISOString().substr(11, 12)
    return date + ' ' + time
}

module.exports = newTimestamp
