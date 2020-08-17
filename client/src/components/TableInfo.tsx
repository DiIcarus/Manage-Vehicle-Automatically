import React from 'react'
/*
* - input : data, function to C_R_UD data
* - output: view (Read)
*/ 
interface Props{
  data:any,
  create?:()=>void,
  del?:()=>void,
  update?:()=>void,
}
const TableInfo = (props:Props) => {
  const {data, create, update, del} = props
  const renderFeatureBar=()=>{
    return(
      <React.Fragment>
      
      </React.Fragment>
    )
  }
  return (
    <React.Fragment>

    </React.Fragment>
  )
}

export default TableInfo
interface rowProps{
  data:any[]
}
const renderRow=(props:rowProps)=>{
  const {data} = props
  return (
    <React.Fragment>
    </React.Fragment>
  )

}

