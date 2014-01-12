namespace Modbus
{
    public enum ModbusMap
    {
        ADR_STAT = 0,
        ADR_ADC_STATUS = 1,
        ADR_ADC_RESULT = 2,
        ADR_ADC_ENG_VALUES = 12,
        ADR_TIME = 22,
        ADR_BINR_STATE = 25,
        ADR_BINR_ATTEMPTNUM = 26,
        ADR_STATUS_IOBSM = 27,
        ADR_DIVISOR_COUNTER = 28,
        ADR_TIME_SCALE = 29,
        ADR_GSK_RES = 55,
        ADR_OSK_RES = 83,
        ADR_CMD = 111,
        ADR_NAV_TASK_STATE = 112,
        ADR_NAV_TASK_CORRECT_ORB_INDEX = 113,
        ADR_NAV_TASK_CORRECT_ORB_INDEX_ITER = 114,
        ADR_COUNT_GPS_DATA = 115,
        ADR_COUNT_GPS_TASK = 116,
        ADR_FIRST_INDEX_GPS_TASK = 117,
        ADR_IS_CYCLE_READ_MODE = 118,
        ADR_STORE_DIVISOR = 119
    }
}