///删除数组中的某一项
function RemoveObjFromArray(array, index) {
    if (index > array.length / 2) {
        for (var i = index; i < this.length - 1; ++i) {
            array[i] = array[i + 1];
        }
        array.pop();
    }
    else {
        for (var i = index; i > 0; --i) {
            array[i] = array[i - 1];
        }
        array.shift();
    }
};