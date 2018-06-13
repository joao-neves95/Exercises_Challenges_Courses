// My implementation of a simple dictionary.

class Errors {
  static get existingKey() { return 'An item with the same key has already been added.' };
}

class Dictionary {
  constructor(uniqueKeys) {
    this.elements = [];

    this.uniqueKeys = uniqueKeys;
    if (!uniqueKeys) this.uniqueKeys = false;
  }
    
  get count() {
    return this.elements.length;
  };

  add(key, value) {
    console.debug(this.findIndexOfKey(key))
    if (this.uniqueKeys && this.findIndexOfKey(key) !== undefined)
      throw new Error(Errors.existingKey);

    this.elements.push({ [key]: value });
  };

  remove(key) {
    this.elements.splice(this.findIndexOfKey(key), 1);
  };

  get clear() {
    this.elements = [];
  }

  getByKey (key) {
    return this.elements[this.findIndexOfKey(key)][key];
  };

  findIndexOfKey(key, Callback) {
    for (let i = 0; i < this.elements.length; i++) {
      if (Object.keys(this.elements[i])[0] === key) {
        return i;
      }
    }
  }
}
