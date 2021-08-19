function getDataMonth(jsonarray, mode) {
    const gastos = jsonarray
    let data = new Array(12).fill(0)
    for (gasto in gastos) {
        let fecha = Date.parse(gastos[gasto].fecha)

        var monto = gastos[gasto].monto
        if (mode == 0) { monto = -Math.abs(monto) }
        let month = new Date(fecha).getMonth()
        switch (month) {
            case 0:
                data[0] += monto
                break
            case 1:
                data[1] += monto
                break
            case 2:
                data[2] += monto
            case 3:
                data[3] += monto
                break
            case 4:
                data[4] += monto
                break
            case 5:
                data[5] += monto
                break
            case 6:
                data[6] += monto
                break
            case 7:
                data[7] += monto
                break
            case 8:
                data[8] += monto
            case 9:
                data[9] += monto
            case 10:
                data[10] += monto
            case 11:
                data[11] += monto
                break
        }
    }
    return data
}