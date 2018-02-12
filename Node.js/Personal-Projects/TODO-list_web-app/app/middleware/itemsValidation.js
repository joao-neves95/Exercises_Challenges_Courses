'use strict'
const Ajv = require('ajv');
const ajv = new Ajv({ allErrors: true });

const itemsSchema = {
    //24
  "type": "object",
  "required": ["title", "priority"],
  "properties": {
    "title": { "type": "string", "minLength": 1, "maxLength": 100 },
    "priority": { "type": "string", "pattern": "^([1-5])$", "minLength": 1, "maxLength": 1 },
    "description": { "type": "string" },
    "dueDate": { "type": "string"},
    "dueTime": { "type": "string"}
  }
}
let validSchema = ajv.compile(itemsSchema)

module.exports = (req, res, next) => {
  let userInput = {
    "title": req.body.title,
    "priority": req.body.priority,
    "description": req.body.description,
    "dueDate": req.body.dueDate,
    "dueTime": req.body.dueTime
    }

  let validateSchema = validSchema(userInput)
  if (!validateSchema) {
    console.log("Error", validSchema.errors)
    return res.status(400).json({ "msg": "Wrong input.", "Error": validSchema.errors });
  } else {
    return next();
  }
}
