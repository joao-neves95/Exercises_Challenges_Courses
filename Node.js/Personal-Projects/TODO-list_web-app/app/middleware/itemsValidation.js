'use strict'
const Ajv = require('ajv');
const ajv = new Ajv({ allErrors: true });

const itemsSchema = {
  "type": "object",
  "required": ["title", "priority"],
  "properties": {
    "title": { "type": "string" },
    "priority": { "type": "string", "pattern": "^([1-5])$", "maxLength": 1 },
    "description": { "type": "string" },
    "dueDate": { "type": "string"/*, "format": "date" */},
    "dueTime": { "type": "string"/*, "format": "date" */}
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
