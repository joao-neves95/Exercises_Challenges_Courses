module.exports = {
  
  whoami (req, res) {
    let ipAddress = req.headers["x-forwarded-for"].split(',', 1).join()
    let software = req.headers["user-agent"].split(/\(|\)/)
    let language = req.headers["accept-language"].split(',', 1).join()
    
    res.status(200).send({
      "ipAddress": ipAddress,
      "software": software[1],
      "language": language
    })
  }
  
}
