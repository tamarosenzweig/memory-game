import { ValidatorFn } from '@angular/forms';

/**
* @method 
* @param cntName the control name in the fromGroup
* @param min minimum chars the control must contain
* @param max maximum chars the control can contain
* @param pattern the pattern of the string 
* @return array of validators
*/
export function stringValidatorArr(cntName: string, min?: number, max?: number, pattern?: RegExp): Array<ValidatorFn> {
   return [
     f => !f.value ? { "val": `${cntName} is required` } : null,
     f => f.value && max && f.value.length > max ? { "val": `${cntName} is max ${max} chars` } : null,
     f => f.value && min && f.value.length < min ? { "val": `${cntName} is min ${min} chars` } : null,
     f => f.value && pattern && !f.value.match(pattern) ? { "val": `${cntName} is contain only english letter` } : null
   ];
 }

 /**
* @method 
* @param cntName the control name in the fromGroup
* @param min min value the control must contain
* @param max max value the control can contain
* @return array of validators
*/
 export function numberValidatorArr(cntName: string, min?: number, max?: number): Array<ValidatorFn> {
    return [
      f => !f.value ? { "val": `${cntName} is required` } : null,
      f => f.value && max && f.value > max ? { "val": `${cntName} can be maximum ${max}` } : null,
      f => f.value && min && f.value< min ? { "val": `${cntName} must be minimum ${min}` } : null,
    ];
  }