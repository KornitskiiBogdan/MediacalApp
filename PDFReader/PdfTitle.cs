using System.Threading.Tasks;

namespace PDFReader;

internal class PdfTitle
{
    private Dictionary<Title, List<RowDocument>> rows;

    public PdfTitle()
    {
        rows = new Dictionary<Title, List<RowDocument>>();
    }

    public void AddRow(RowDocument row)
    {
        if (rows.TryGetValue(row.Title, out var list))
        {
            list.Add(row);
        }
        else
        {
            rows.Add(row.Title, new List<RowDocument>(){row});
        }
    }

    public RowDocument[]? GetTitle()
    {
        int minDistance = int.MaxValue;
        (RowDocument? mark, RowDocument? value, RowDocument? unit) = (null, null, null);
        if (rows.TryGetValue(Title.Mark, out var markList) 
            && rows.TryGetValue(Title.Unit, out var unitList) 
            && rows.TryGetValue(Title.Value, out var valueList))
        {
            foreach (var m in markList)
            {
                foreach (var u in unitList)
                {
                    foreach (var v in valueList)
                    {

                        var distanceMV = Math.Abs(m.IterationFinding - v.IterationFinding);
                        var distanceMU = Math.Abs(m.IterationFinding - u.IterationFinding);
                        if (distanceMV < distanceMU)
                        {
                            var distance = distanceMV + Math.Min(Math.Abs(u.IterationFinding - v.IterationFinding), distanceMU);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                mark = m;
                                value = v;
                                unit = u;
                            }
                        }
                        else
                        {
                            var distance = distanceMU + Math.Min(Math.Abs(u.IterationFinding - v.IterationFinding), distanceMV);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                mark = m;
                                value = v;
                                unit = u;
                            }
                        }
                    }
                }
            }
        }

        if (mark is null || value is null || unit is null)
        {
            return null;
        }

        if (mark.IterationFinding < value.IterationFinding && mark.IterationFinding < unit.IterationFinding)
        {
            return value.IterationFinding < unit.IterationFinding
                ? new RowDocument[3] { mark, value, unit }
                : new RowDocument[3] { mark, unit, value };
        }
        else if (value.IterationFinding < mark.IterationFinding && value.IterationFinding < unit.IterationFinding)
        {
            return mark.IterationFinding < unit.IterationFinding
                ? new RowDocument[3] { value, mark, unit }
                : new RowDocument[3] { value, unit, mark };
        }
        else
        {
            return mark.IterationFinding < value.IterationFinding
                ? new RowDocument[3] { unit, mark, value }
                : new RowDocument[3] { unit, value, mark };
        }

    }
}