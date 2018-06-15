class Errors {
  static get existingKey() { return 'An item with the same key has already been added.' };

  static wrongType(type) { return `The value is not from the same type as the List<${type}>` };
}

class Dictionary {
  /**
   * 
   * @param {Boolean} uniqueKeys
   * Whether the keys should be unique or not.
   * Optional. It defaults to false.
   */
  constructor(uniqueKeys) {
    this.elements = [];

    this.uniqueKeys = uniqueKeys;
    if (!uniqueKeys) this.uniqueKeys = false;
  };

  get all() {
    return this.elements;
  }

  get length() {
    return this.elements.length;
  };

  add(key, value) {
    if (this.uniqueKeys && this.findIndexOfKey(key) !== undefined)
      throw new Error(Errors.existingKey);

    this.elements.push({ [key]: value });
  };

  remove(key) {
    const index = this.findIndexOfKey(key);
    if (!index)
      return false;
  
    this.elements.splice(index, 1);
  };

  clear() {
    this.elements = [];
  }

  getByIndex(index) {
    return Object.values(this.elements[index]);
  };

  getByKey(key) {
    return this.elements[this.findIndexOfKey(key)][key];
  };

  findIndexOfKey(key, Callback) {
    for (let i = 0; i < this.elements.length; i++) {
      if (Object.keys(this.elements[i])[0] === key) {
        return i;
      }
    }
    return false;
  }
}
