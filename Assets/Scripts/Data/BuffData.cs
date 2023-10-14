using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData 
{
    public string buff名字;
    public string buff提升的属性名;//多属性提升改为字典
    public float buff提升的属性值;
    public int 层数;
    public int 最大层数;
    public float 持续时间;
    public bool 是否可以叠加;

    public BuffData(string buff名字,string buff提升的属性名, float buff提升的属性值,int 层数, int 最大层数, float 持续时间,bool 是否可以叠加) {
        this.buff名字 = buff名字;
        this.buff提升的属性名 = buff提升的属性名;
        this.buff提升的属性值 = buff提升的属性值;
        this.层数 = 层数;
        this.最大层数 = 最大层数;
        this.持续时间 = 持续时间;
        this.是否可以叠加 = 是否可以叠加;
    }
}
